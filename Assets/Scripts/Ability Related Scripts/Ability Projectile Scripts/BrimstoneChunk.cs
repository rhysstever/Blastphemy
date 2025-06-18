using UnityEngine;

public class BrimstoneChunk : AbilityProjectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        BrimstoneAbility brimstoneAbility = AbilityManager.instance.GetAbility(Ability.Brimstone) as BrimstoneAbility;
        damage = brimstoneAbility.Damage;
        lifeSpan = brimstoneAbility.ChunkLifeSpan;
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
