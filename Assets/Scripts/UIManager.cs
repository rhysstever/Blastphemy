using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static UIManager instance = null;

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
    private GameObject mainMenuUIParent, gameUIParent, abilitySelectUIParent, pauseUIParent, endUIParent;
    [SerializeField]
    private Button mainMenuToGameButton, pauseToGameButton, pauseToMainMenuButton;
    [SerializeField]
    private List<Button> abilitySelectButtons;
    [SerializeField]
    private List<TMP_Text> abilityNames, abilityDescriptions, abilityFlavorTexts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUIState(MenuState newMenuState) {
        switch(newMenuState) {
            case MenuState.MainMenu:
                Reset();
                break;
            case MenuState.Game:
                mainMenuUIParent.SetActive(false);
                abilitySelectUIParent.SetActive(false);
                pauseUIParent.SetActive(false);
                gameUIParent.SetActive(true);
                break;
            case MenuState.AbilitySelect:
                mainMenuUIParent.SetActive(false);  // Needed for inital play
                abilitySelectUIParent.SetActive(true);
                pauseUIParent.SetActive(false);
                UpdateAbilitySelects();
                break;
            case MenuState.Pause:
                pauseUIParent.SetActive(true);
                break;
            case MenuState.End:
                gameUIParent.SetActive(false);
                endUIParent.SetActive(true);
                break;
        }
    }

    private void SetupButtons() {
        mainMenuToGameButton.onClick.AddListener(() => { GameManager.instance.ChangeMenuState(MenuState.Game); });
        pauseToGameButton.onClick.AddListener(() => { GameManager.instance.ChangeMenuState(MenuState.Game); });
        pauseToMainMenuButton.onClick.AddListener(() => { GameManager.instance.ChangeMenuState(MenuState.MainMenu); });
    }

    private void Reset() {
        mainMenuUIParent.SetActive(true);
        gameUIParent.SetActive(false);
        abilitySelectUIParent.SetActive(false);
        pauseUIParent.SetActive(false);
        endUIParent.SetActive(false);
    }

    private void UpdateAbilitySelects() {
        List<BaseAbility> randomAbilties = AbilityManager.instance.GetRandomAbilities(3);

        for(int i = 0; i < abilityNames.Count; i++) {
            abilityNames[i].text = randomAbilties[i].AbilityName;
            abilityDescriptions[i].text = randomAbilties[i].AbilityDescription;
            abilityFlavorTexts[i].text = randomAbilties[i].FlavorText;

            Ability abilityType = randomAbilties[i].AbilityType;

            // Setup corresponding button
            abilitySelectButtons[i].onClick.RemoveAllListeners();
            abilitySelectButtons[i].onClick.AddListener(() => { 
                AbilityManager.instance.UpgradeAbility(abilityType); 
                GameManager.instance.ChangeMenuState(MenuState.Game);
            });
        }
    }
}
