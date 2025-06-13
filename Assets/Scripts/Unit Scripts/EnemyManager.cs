using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private float spawnRange;
    [SerializeField]
    private GameObject basicEnemyPrefab;

    private float spawnTimer, currentSpawnTimer;

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
        if(GameManager.instance.CurrentGameState == GameState.Game) {
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

        Instantiate(enemy, newPosition, Quaternion.identity, transform);
    }
}
