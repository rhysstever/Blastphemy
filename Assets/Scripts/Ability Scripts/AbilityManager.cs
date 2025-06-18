using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Ability
{
    Apocalypse,
    Brimstone,
    Immolation,
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
    private List<BaseAbility> abilities;
    private Dictionary<Ability, BaseAbility> abilityMap;

    private int currentLevel;
    private float currentXP;
    private List<float> xpNeededForNextLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityMap = new Dictionary<Ability, BaseAbility>();
        foreach(BaseAbility ability in abilities) {
            abilityMap.Add(ability.AbilityType, ability);
        }

        currentLevel = 0;
        currentXP = 0;
        xpNeededForNextLevel = new List<float>();
        xpNeededForNextLevel.Add(0);
        xpNeededForNextLevel.Add(30);
        xpNeededForNextLevel.Add(60);
        xpNeededForNextLevel.Add(120);
        xpNeededForNextLevel.Add(200);
        xpNeededForNextLevel.Add(350);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BaseAbility GetAbility(Ability ability) {
        return abilityMap[ability];
    }

    public void UpgradeAbility(Ability abilityType) {
        abilityMap[abilityType].Upgrade();
    }

    public List<BaseAbility> GetRandomAbilities(int numberOfAbilities, List<BaseAbility> alreadySelectedAbilities) {
        if(numberOfAbilities < 1) { 
            return alreadySelectedAbilities;
        } else {
            List<BaseAbility> remainingAbilities = abilities.Where(a => !alreadySelectedAbilities.Contains(a)).ToList();

            BaseAbility randomAbility = remainingAbilities[Random.Range(0, remainingAbilities.Count)];
            alreadySelectedAbilities.Add(randomAbility);

            return GetRandomAbilities(numberOfAbilities - 1, alreadySelectedAbilities);
        }
    }

    public void AddXP(float xp) {
        if(xp >= 0f) {
            currentXP += xp;

            CheckLevelUp();
        }
    }

    private void CheckLevelUp() {
        float xpForNextLevel = xpNeededForNextLevel[currentLevel];

        if(currentXP >= xpForNextLevel) {
            currentXP -= xpForNextLevel;
            GameManager.instance.ChangeMenuState(MenuState.AbilitySelect);
            currentLevel++;
        }
    }
}
