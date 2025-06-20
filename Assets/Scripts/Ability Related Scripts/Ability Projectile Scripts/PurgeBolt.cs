using UnityEngine;

public class PurgeBolt : DynamicAbilityProjectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start() 
    {
        base.Start();

        PurgeAbility purgeAbility = AbilityManager.instance.GetAbility(AbilityType.Purge) as PurgeAbility;
        damage = purgeAbility.Damage;
        lifeSpan = purgeAbility.BoltLifeSpan;
    }

    // Update is called once per frame
    protected override void Update() 
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Enemy")) {
            collider.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
