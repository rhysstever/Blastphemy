using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum Ability
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
    private List<BaseAbility> abilityList;

    private Dictionary<Ability, BaseAbility> abilityMap;

    public List<BaseAbility> AbilityList { get { return abilityList; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityMap = new Dictionary<Ability, BaseAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) {
            UpgradeAbility("Brimstone");
        }
    }

    public BaseAbility GetRandomAbility(bool onlyActiveAbilities) {
        List<BaseAbility> filteredAbilityList;
        if(onlyActiveAbilities) {
            filteredAbilityList = abilityList.Where(a => a.Level == 0).ToList();
        } else {
            filteredAbilityList = abilityList;
        }

        int randomIndex = Random.Range(0, filteredAbilityList.Count);
        return filteredAbilityList[randomIndex];
    }

    public string GetAbilityDescription(string abilityName) {
        BaseAbility ability = GetAbilityByName(abilityName);

        if(ability != null) {
            return ability.AbilityDescription;
        } else {
            return "";
        }
    }

    public void UpgradeAbility(string abilityName) {
        BaseAbility ability = GetAbilityByName(abilityName);

        if(ability != null) {
            ability.Upgrade();
        }
    }

    private BaseAbility GetAbilityByName(string abilityName) {
        foreach(BaseAbility ability in abilityList) {
            if(ability.AbilityName == abilityName) {
                return ability;
            }
        }

        Debug.Log(string.Format("Error! No ability named {0}", abilityName));
        return null;
    }
}
