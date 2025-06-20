using UnityEngine;

public class ScourgeAbility : BaseAbility
{
    [SerializeField]
    private GameObject sourgeColliderObject;
    private float range;

    public float Range { get { return range; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        abilityType = AbilityType.Scourge;
        abilityName = "Scourge";
        abilityDescription = "Emit a small holy aura that damages enemies.";
        flavorText = "...when the overflowing scourge shall pass through, then ye shall be trodden down by it. - Isaiah 28:18";

        damage = 0.01f;
        cooldown = 0f;

        range = 5f;
        sourgeColliderObject.transform.localScale = Vector3.one * range;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        sourgeColliderObject.SetActive(CanCast());
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
        }
    }
}
