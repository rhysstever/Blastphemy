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
        // For each ability in the abilities list, map it to its AbilityType
        abilityMap = new Dictionary<Ability, BaseAbility>();
        foreach(BaseAbility ability in abilities) {
            abilityMap.Add(ability.AbilityType, ability);
        }

        SetupLevels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Get an Ability (script) based on the given Ability type (enum value)
    /// </summary>
    /// <param name="ability">The ability type</param>
    /// <returns>An Ability that can be cast as a child BaseAbility</returns>
    public BaseAbility GetAbility(Ability ability) {
        return abilityMap[ability];
    }

    /// <summary>
    /// Upgrade an Ability to the next level
    /// </summary>
    /// <param name="abilityType">The ability type</param>
    public void UpgradeAbility(Ability abilityType) {
        abilityMap[abilityType].Upgrade();
    }

    /// <summary>
    /// Gets a list of random, unique abilities
    /// </summary>
    /// <param name="numberOfAbilities">The number of random abilties to get</param>
    /// <param name="alreadySelectedAbilities">A list of abilities already selected that should be acquired again</param>
    /// <returns>A list of Abilities, each that can be cast to a specific child Ability</returns>
    public List<BaseAbility> GetRandomAbilities(int numberOfAbilities, List<BaseAbility> alreadySelectedAbilities) {
        if(numberOfAbilities < 1) { 
            return alreadySelectedAbilities;
        } else {
            List<BaseAbility> remainingAbilities = abilities.Where(a => !alreadySelectedAbilities.Contains(a)).ToList();

            if(remainingAbilities.Count == 0) {
                alreadySelectedAbilities.Add(abilities[0]);
            } else {
                BaseAbility randomAbility = remainingAbilities[Random.Range(0, remainingAbilities.Count)];
                alreadySelectedAbilities.Add(randomAbility);
            }

            return GetRandomAbilities(numberOfAbilities - 1, alreadySelectedAbilities);
        }
    }

    /// <summary>
    /// Give XP to the player
    /// </summary>
    /// <param name="xp">The amount of experience</param>
    public void AddXP(float xp) {
        if(xp >= 0f) {
            currentXP += xp;

            CheckLevelUp();
        }
    }

    /// <summary>
    /// Set up initial level and xp; along with the amount of xp needed to level up
    /// </summary>
    private void SetupLevels() {
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

    /// <summary>
    /// Check if the player has enough xp to level up
    /// </summary>
    private void CheckLevelUp() {
        float xpForNextLevel = xpNeededForNextLevel[currentLevel];

        if(currentXP >= xpForNextLevel) {
            currentXP -= xpForNextLevel;
            GameManager.instance.ChangeMenuState(MenuState.AbilitySelect);
            currentLevel++;
        }
    }
}
