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
    private GameObject mainMenuUIParent, upgradesUIParent, gameUIParent, abilitySelectUIParent, pauseUIParent, endUIParent;
    [SerializeField]
    private Button mainMenuToUpgradesButton, upgradesToGameButton, pauseToGameButton, pauseToMainMenuButton, endToUpgradesButton;
    [SerializeField]
    private TMP_Text gameTimerText, levelText;
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
        UpdateUI(GameManager.instance.CurrentMenuState);
    }

    /// <summary>
    /// Perform one-time UI updates based on a menu state
    /// </summary>
    /// <param name="newMenuState">The current menu state of the game</param>
    public void ChangeUI(MenuState newMenuState) {
        switch(newMenuState) {
            case MenuState.MainMenu:
                Reset();
                break;
            case MenuState.Upgrades:
                HideAllButOneMenuUI(MenuState.Upgrades);
                break;
            case MenuState.Game:
                HideAllButOneMenuUI(MenuState.Game);
                break;
            case MenuState.AbilitySelect:
                abilitySelectUIParent.SetActive(true);
                pauseUIParent.SetActive(false);
                UpdateAbilitySelectUI();
                break;
            case MenuState.Pause:
                pauseUIParent.SetActive(true);
                break;
            case MenuState.End:
                HideAllButOneMenuUI(MenuState.End);
                break;
        }
    }

    public void UpdateUI(MenuState newMenuState) {
        switch(newMenuState) {
            case MenuState.MainMenu:
                break;
            case MenuState.Upgrades:
                break;
            case MenuState.Game:
                UpdateGameTimerText();
                break;
            case MenuState.AbilitySelect:
                break;
            case MenuState.Pause:
                break;
            case MenuState.End:
                break;
        }
    }

    /// <summary>
    /// Setup onClicks for each button
    /// </summary>
    private void SetupButtons() {
        mainMenuToUpgradesButton.onClick.AddListener(() => { GameManager.instance.ChangeMenuState(MenuState.Upgrades); });
        upgradesToGameButton.onClick.AddListener(() => { GameManager.instance.ChangeMenuState(MenuState.Game); });
        pauseToGameButton.onClick.AddListener(() => { GameManager.instance.ChangeMenuState(MenuState.Game); });
        pauseToMainMenuButton.onClick.AddListener(() => { GameManager.instance.ChangeMenuState(MenuState.MainMenu); });
        endToUpgradesButton.onClick.AddListener(() => { GameManager.instance.ChangeMenuState(MenuState.Upgrades); });
    }

    /// <summary>
    /// Reset the game's UI
    /// </summary>
    private void Reset() {
        HideAllButOneMenuUI(MenuState.MainMenu);
    }

    private void HideAllButOneMenuUI(MenuState menuStateToShow) {
        mainMenuUIParent.SetActive(menuStateToShow == MenuState.MainMenu);
        upgradesUIParent.SetActive(menuStateToShow == MenuState.Upgrades);
        gameUIParent.SetActive(menuStateToShow == MenuState.Game);
        abilitySelectUIParent.SetActive(menuStateToShow == MenuState.AbilitySelect);
        pauseUIParent.SetActive(menuStateToShow == MenuState.Pause);
        endUIParent.SetActive(menuStateToShow == MenuState.End);
    }

    /// <summary>
    /// Update the UI for each ability select groups
    /// </summary>
    private void UpdateAbilitySelectUI() {
        List<BaseAbility> randomAbilties = AbilityManager.instance.GetRandomAbilities(3, new List<BaseAbility>());

        for(int i = 0; i < abilityNames.Count; i++) {
            // Display the text for each ability
            abilityNames[i].text = randomAbilties[i].AbilityName;
            abilityDescriptions[i].text = randomAbilties[i].AbilityDescription;
            abilityFlavorTexts[i].text = randomAbilties[i].FlavorText;

            AbilityType abilityType = randomAbilties[i].AbilityType;

            // Setup corresponding button
            abilitySelectButtons[i].onClick.RemoveAllListeners();
            abilitySelectButtons[i].onClick.AddListener(() => { 
                AbilityManager.instance.UpgradeAbility(abilityType); 
                GameManager.instance.ChangeMenuState(MenuState.Game);
            });
        }
    }

    /// <summary>
    /// Update the game clock
    /// </summary>
    private void UpdateGameTimerText() {
        gameTimerText.text = GameManager.instance.GetGameTime();
    }

    public void UpdateLevelText(string title, int level) {
        levelText.text = string.Format("{0} {1}", title, level);
    }
}
