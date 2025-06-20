using System.Collections;
using UnityEngine;

public class BrimstoneAbility : BaseAbility
{
    [SerializeField]
    private GameObject chunkPrefab;

    private int chunks;
    private float range, chunkSpawnDelay, chunkLifeSpan;

    public float Range { get { return range; } }
    public float ChunkLifeSpan { get { return chunkLifeSpan; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        abilityType = AbilityType.Brimstone;
        abilityName = "Brimstone";
        abilityDescription = "Rain large chunks of fire that deal area damage around you.";
        flavorText = "Upon the wicked he shall rain snares, fire and brimstone, and an horrible tempest: this shall be the portion of their cup. - Psalm 11:6";

        damage = 2f;
        cooldown = 7f;

        chunks = 3;
        range = 8f;
        chunkSpawnDelay = 0.5f;
        chunkLifeSpan = 2f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Cast() {
        if(CanCast()) {
            base.Cast();

            for(int i = 0; i < chunks; i++) {
                StartCoroutine(SpawnChunk(i * chunkSpawnDelay));
            }
        }
    }

    IEnumerator SpawnChunk(float totalDelay) {
        yield return new WaitForSeconds(totalDelay);

        float randomAngle = Random.Range(0, 360f);
        Vector2 newPosition = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle));
        newPosition *= range;
        newPosition += (Vector2)transform.position;

        Instantiate(chunkPrefab, newPosition, Quaternion.identity, GameManager.instance.BulletParent);
    }
}
