using UnityEngine;

public class ImmolationFlame : DynamicAbilityProjectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        ImmolationAbility immolationAbility = AbilityManager.instance.GetAbility(AbilityType.Immolation) as ImmolationAbility;
        damage = immolationAbility.Damage;
        lifeSpan = immolationAbility.FlameLifeSpan;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Enemy")) {
            collider.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
        }
    }
}
