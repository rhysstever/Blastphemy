using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    MainMenu,
    Game,
    AbilitySelect,
    Pause,
    End
}

public class GameManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static GameManager instance = null;

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
    private Transform bulletParent, enemyParent;
    [SerializeField]
    private GameObject playerObject;

    private Stack<MenuState> menuStates;

    public Transform BulletParent { get { return bulletParent; } }
    public Transform EnemyParent { get { return enemyParent;  } }
    public MenuState CurrentMenuState { get { return menuStates.Peek(); } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuStates = new Stack<MenuState>();
        ChangeMenuState(MenuState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && menuStates.Peek() == MenuState.Game) {
            ChangeMenuState(MenuState.AbilitySelect);
        }

        UpdateMenuState(menuStates.Peek());
    }

    public void ChangeMenuState(MenuState newMenuState) {
        switch(newMenuState) {
            case MenuState.MainMenu:
                menuStates.Clear();
                break;
            case MenuState.Game:
                menuStates.Clear();
                break;
            case MenuState.AbilitySelect:
                break;
            case MenuState.Pause:
                break;
            case MenuState.End:
                menuStates.Clear();
                break;
        }

        menuStates.Push(newMenuState);
        UIManager.instance.UpdateUIState(newMenuState);
    }

    public void ChangeMenuStateToPreviousMenu() {
        if(menuStates.Count > 1) {
            menuStates.Pop();
            MenuState previousMenuState = menuStates.Pop();
            ChangeMenuState(previousMenuState);
        } else {
            Debug.Log("Error! No new menu state to pop to");
        }
    }

    public void UpdateMenuState(MenuState newMenuState) {
        switch(newMenuState) {
            case MenuState.MainMenu:
                break;
            case MenuState.Game:
                if(playerObject.GetComponent<PlayerControls>().IsPauseClicked()) {
                    ChangeMenuState(MenuState.Pause);
                }
                break;
            case MenuState.AbilitySelect:
                if(playerObject.GetComponent<PlayerControls>().IsPauseClicked()) {
                    ChangeMenuState(MenuState.Pause);
                }
                break;
            case MenuState.Pause:
                if(playerObject.GetComponent<PlayerControls>().IsPauseClicked()) {
                    ChangeMenuStateToPreviousMenu();
                }
                break;
            case MenuState.End:
                break;
        }
    }

    public Vector2 GetPlayerPosition() {
        return playerObject.transform.position;
    }

    public Vector2 GetPlayerAim() {
        return playerObject.GetComponent<PlayerCombat>().ShootDirection;
    }
}
