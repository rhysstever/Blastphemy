using UnityEngine;

public enum GameState
{
    MainMenu,
    Game,
    AbilitySelect,
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

    [SerializeField]
    private GameState currentGameState;

    public Transform BulletParent { get { return bulletParent; } }
    public Transform EnemyParent { get { return enemyParent;  } }
    public GameObject PlayerObject { get { return playerObject;} }
    public GameState CurrentGameState { get { return currentGameState; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeGameState(GameState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && currentGameState == GameState.Game) {
            ChangeGameState(GameState.AbilitySelect);
        }
    }

    public void ChangeGameState(GameState newGameState) {
        switch(newGameState) {
            case GameState.MainMenu:
                break;
            case GameState.Game:
                break;
            case GameState.AbilitySelect:
                break;
            case GameState.End:
                break;
        }

        currentGameState = newGameState;
        UIManager.instance.UpdateUIState(newGameState);
    }

    public Vector2 GetPlayerPosition() {
        return playerObject.transform.position;
    }
}
