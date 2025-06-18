using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static EnemyManager instance = null;

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
    private Transform enemyParent;
    [SerializeField]
    private float spawnRange;
    [SerializeField]
    private GameObject basicEnemyPrefab;

    private float spawnTimer, currentSpawnTimer;

    public Transform EnemyParent { get { return enemyParent; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = 2f;
        currentSpawnTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if(GameManager.instance.CurrentMenuState == MenuState.Game) {
            currentSpawnTimer += Time.deltaTime;

            if(currentSpawnTimer >= spawnTimer) {
                SpawnEnemy(basicEnemyPrefab);
                currentSpawnTimer = 0f;
            }
        }
    }

    public void SpawnEnemy(GameObject enemy) {
        float randomAngle = Random.Range(0, 360f);
        Vector2 newPosition = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle));
        newPosition *= spawnRange;
        newPosition += GameManager.instance.GetPlayerPosition();

        Instantiate(enemy, newPosition, Quaternion.identity, enemyParent);
    }
}
