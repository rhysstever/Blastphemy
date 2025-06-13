using System.Collections.Generic;
using UnityEngine;

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

    public List<BaseAbility> AbilityList { get { return abilityList; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) {
            UpgradeAbility("Brimstone");
        }
    }

    public void UpgradeAbility(string abilityName) {
        foreach(BaseAbility ability in abilityList) {
            if(ability.AbilityName == abilityName) {
                ability.Upgrade();
                return;
            }
        }

        Debug.Log(string.Format("Error! No ability named {0}", abilityName));
    }
}
