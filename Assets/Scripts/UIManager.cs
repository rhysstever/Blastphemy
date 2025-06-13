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
    private GameObject mainMenuUIParent, gameUIParent, abilitySelectUIParent, endUIParent;
    [SerializeField]
    private Button mainMenuToGameButton;
    [SerializeField]
    private List<TMP_Text> abilityNames, abilityDescriptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUIState(GameState newGameState) {
        switch(newGameState) {
            case GameState.MainMenu:
                Reset();
                break;
            case GameState.Game:
                mainMenuUIParent.SetActive(false);
                gameUIParent.SetActive(true);
                break;
            case GameState.AbilitySelect:
                abilitySelectUIParent.SetActive(true);
                UpdateAbilitySelects();
                break;
            case GameState.End:
                gameUIParent.SetActive(false);
                endUIParent.SetActive(true);
                break;
        }
    }

    private void SetupButtons() {
        mainMenuToGameButton.onClick.AddListener(() => { GameManager.instance.ChangeGameState(GameState.Game); });
    }

    private void Reset() {
        mainMenuUIParent.SetActive(true);
        gameUIParent.SetActive(false);
        abilitySelectUIParent.SetActive(false);
        endUIParent.SetActive(false);
    }

    private void UpdateAbilitySelects() {
        for(int i = 0; i < abilityNames.Count; i++) {
            // Get random ability
            BaseAbility randomAbility = AbilityManager.instance.GetRandomAbility(false);

            abilityNames[i].text = randomAbility.AbilityName;
            abilityDescriptions[i].text = string.Format(
                    "{0}\n\n{1}",
                    randomAbility.AbilityDescription,
                    randomAbility.FlavorText);
        }
    }
}
