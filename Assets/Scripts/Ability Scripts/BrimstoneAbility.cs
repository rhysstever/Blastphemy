using System.Collections;
using UnityEngine;

public class BrimstoneAbility : BaseAbility
{
    [SerializeField]
    private GameObject chunkPrefab;

    private int chunks;
    private float chunkSpawnDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityName = "Brimstone";
        abilityDescription = "Rain large chunks of fire that deal area damage around you.";
        flavorText = "Upon the wicked he shall rain snares, fire and brimstone, and an horrible tempest: this shall be the portion of their cup. - Psalm 11:6";

        level = 1;
        range = 8f;
        damage = 2f;
        cooldown = 7f;

        chunks = 3;
        chunkSpawnDelay = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.CurrentGameState == GameState.Game) {
            currentCooldownTimer += Time.deltaTime;

            if(currentCooldownTimer >= cooldown) {
                Cast();
            }
        }
    }

    public override void Cast() {
        base.Cast();

        for(int i = 0; i < chunks; i++) {
            StartCoroutine(SpawnChunk((i + 1) * chunkSpawnDelay));
        }
    }

    IEnumerator SpawnChunk(float totalDelay) {
        yield return new WaitForSeconds(totalDelay);

        float randomAngle = Random.Range(0, 360f);
        Vector2 newPosition = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle));
        newPosition *= range;
        newPosition += (Vector2)transform.position;

        Instantiate(chunkPrefab, newPosition, Quaternion.identity);
    }
}
