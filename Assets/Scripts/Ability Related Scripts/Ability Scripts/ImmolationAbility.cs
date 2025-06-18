using System.Collections;
using UnityEngine;

public class ImmolationAbility : BaseAbility
{
    [SerializeField]
    private GameObject flamePrefab;

    private int flames;
    private float flameProjectileSpeed, flameSpawnDelay, flameLifeSpan;

    public float FlameProjectileSpeed { get { return flameProjectileSpeed; } }
    public float FlameLifeSpan { get { return flameLifeSpan; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        abilityType = Ability.Immolation;
        abilityName = "Immolation";
        abilityDescription = "Shoot columns of fire on either side of you";
        flavorText = "Who maketh his angels spirits; his ministers a flaming fire. - Psalms 104:4";

        damage = 0.25f;
        cooldown = 10f;

        flames = 8;
        flameProjectileSpeed = 10f;
        flameSpawnDelay = 0.25f;
        flameLifeSpan = 5f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Cast() {
        if(CanCast()) {
            base.Cast();

            for(int i = 0; i < flames; i++) {
                StartCoroutine(SpawnFlame(i * flameSpawnDelay));
            }
        }
    }

    IEnumerator SpawnFlame(float totalDelay) {
        yield return new WaitForSeconds(totalDelay);

        Vector2 playerAim = GameManager.instance.GetPlayerAim();

        Vector2 leftAimDirection = new Vector2(playerAim.y, -playerAim.x);
        Vector2 positionLeftFlame = (Vector2)transform.position + leftAimDirection;
        GameObject newLeftFlame = Instantiate(flamePrefab, positionLeftFlame, Quaternion.identity, GameManager.instance.BulletParent);
        newLeftFlame.GetComponent<ImmolationFlame>().SetVelocity(leftAimDirection * flameProjectileSpeed);

        Vector2 rightAimDirection = new Vector2(-playerAim.y, playerAim.x);
        Vector2 positionRightFlame = (Vector2)transform.position + rightAimDirection;
        GameObject newRightFlame = Instantiate(flamePrefab, positionRightFlame, Quaternion.identity, GameManager.instance.BulletParent);
        newRightFlame.GetComponent<ImmolationFlame>().SetVelocity(rightAimDirection * flameProjectileSpeed);
    }
}
