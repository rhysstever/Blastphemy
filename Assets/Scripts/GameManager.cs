using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    MainMenu,
    Upgrades,
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
    private Transform bulletParent;
    [SerializeField]
    private GameObject playerObject;

    private Stack<MenuState> menuStates;

    private float gameTime;

    public Transform BulletParent { get { return bulletParent; } }
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
        UpdateMenuState(menuStates.Peek());
    }

    /// <summary>
    /// Change the menu state of the game and run any one-time code
    /// </summary>
    /// <param name="newMenuState">The new menu state of the game</param>
    public void ChangeMenuState(MenuState newMenuState) {
        switch(newMenuState) {
            case MenuState.MainMenu:
                ShowPlayer(false);
                menuStates.Clear();
                break;
            case MenuState.Upgrades:
                ShowPlayer(false);
                menuStates.Clear();
                break;
            case MenuState.Game:
                ShowPlayer(true);

                // If this is the start of the game, trigger the first ability select screen
                if(menuStates.TryPeek(out MenuState previousMenuState)) {
                    if(previousMenuState == MenuState.Upgrades) {
                        AbilityManager.instance.AddXP(0);
                        return;
                    }
                }

                menuStates.Clear();
                break;
            case MenuState.AbilitySelect:
                break;
            case MenuState.Pause:
                break;
            case MenuState.End:
                ShowPlayer(false);
                menuStates.Clear();
                break;
        }

        // Add the new menu state to the stack
        menuStates.Push(newMenuState);
        // Update UI
        UIManager.instance.ChangeUI(newMenuState);
    }

    /// <summary>
    /// Move back to the previous menu state
    /// </summary>
    public void ChangeMenuStateToPreviousMenu() {
        if(menuStates.Count > 1) {
            menuStates.Pop();
            MenuState previousMenuState = menuStates.Pop();
            ChangeMenuState(previousMenuState);
        } else {
            Debug.Log("Error! No new menu state to pop to");
        }
    }

    /// <summary>
    /// Perform continuous, every frame logic based on the current menu state
    /// </summary>
    /// <param name="newMenuState">The current menu state</param>
    public void UpdateMenuState(MenuState newMenuState) {
        switch(newMenuState) {
            case MenuState.MainMenu:
                break;
            case MenuState.Upgrades:
                break;
            case MenuState.Game:
                if(playerObject.GetComponent<PlayerControls>().IsPauseClicked()) {
                    ChangeMenuState(MenuState.Pause);
                }

                gameTime += Time.deltaTime;
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

    /// <summary>
    /// Get the player's current position
    /// </summary>
    /// <returns>The player object's current world position</returns>
    public Vector2 GetPlayerPosition() {
        return playerObject.transform.position;
    }

    /// <summary>
    /// Get where the player is currently aiming
    /// </summary>
    /// <returns>The normalized direction of the player's aim</returns>
    public Vector2 GetPlayerAim() {
        return playerObject.GetComponent<PlayerMovement>().GetAimDirection();
    }

    /// <summary>
    /// Get the current time in game
    /// </summary>
    /// <returns>A formatted string of the current in game time</returns>
    public string GetGameTime() {
        string minutes = ((int)gameTime / 60).ToString("D2");
        string seconds = ((int)gameTime % 60).ToString("D2");

        return string.Format("{0}:{1}", minutes, seconds);
    }

    /// <summary>
    /// Toggle the visibility of the player in the scene
    /// </summary>
    /// <param name="show">Whether the player should be visible or not</param>
    private void ShowPlayer(bool show) { 
        playerObject.GetComponent<SpriteRenderer>().enabled = show;
        playerObject.transform.GetChild(0).gameObject.SetActive(show);
    }
}
