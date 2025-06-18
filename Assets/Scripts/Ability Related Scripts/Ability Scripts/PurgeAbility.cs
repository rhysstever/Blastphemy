using UnityEngine;

public class PurgeAbility : BaseAbility
{
    [SerializeField]
    private GameObject purgeBoltPrefab;

    private float boltProjectileSpeed, boltLifeSpan;

    public float BoltLifeSpan { get { return boltLifeSpan; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        abilityType = Ability.Purge;
        abilityName = "Purge";
        abilityDescription = "Shoot a bolt forward, damaging .";
        flavorText = "I will purge out from among you the rebels and those who transgress against me. - Ezekiel 20:38";

        damage = 1f;
        cooldown = 1f;

        boltProjectileSpeed = 50f;
        boltLifeSpan = 10f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Cast() {
        if(CanCast()) {
            base.Cast();
            SpawnBolt();
        }
    }

    private void SpawnBolt() {
        Vector2 newPosition = GameManager.instance.GetPlayerPosition() + GameManager.instance.GetPlayerAim();

        GameObject newBolt = Instantiate(purgeBoltPrefab, newPosition, Quaternion.identity, GameManager.instance.BulletParent);
        newBolt.GetComponent<Rigidbody2D>().linearVelocity = GameManager.instance.GetPlayerAim() * boltProjectileSpeed;
    }
}
