using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Ability
{
    Brimstone,
    Purge
}

public class AbilityManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static AbilityManager instance = null;

    // Awake is called even before start
    private void Awake() {
        // If the reference for this script is null, assign it this script
        if(instance == null)
            instance = this;
        // If the reference is to something else (it already exists)
        // than this is not needed, thus destroy it
        else if(instance != this)
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    private BaseAbility brimstoneAbility, purgeAbility;

    private Dictionary<Ability, BaseAbility> abilityMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityMap = new Dictionary<Ability, BaseAbility>();
        abilityMap.Add(Ability.Brimstone, brimstoneAbility);
        abilityMap.Add(Ability.Purge, purgeAbility);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) {
            UpgradeAbility(Ability.Brimstone);
        } else if(Input.GetKeyDown(KeyCode.Space)) {
            UpgradeAbility(Ability.Purge);
        }
    }

    public BaseAbility GetAbility(Ability ability) {
        return abilityMap[ability];
    }

    public BaseAbility GetRandomAbility(bool onlyActiveAbilities) {
        List<BaseAbility> filteredAbilityList;
        if(onlyActiveAbilities) {
            filteredAbilityList = abilityMap.Values.Where(a => a.Level == 0).ToList();
        } else {
            filteredAbilityList = abilityMap.Values.ToList();
        }

        int randomIndex = Random.Range(0, filteredAbilityList.Count);
        return filteredAbilityList[randomIndex];
    }

    public void UpgradeAbility(Ability abilityType) {
        abilityMap[abilityType].Upgrade();
    }
}
