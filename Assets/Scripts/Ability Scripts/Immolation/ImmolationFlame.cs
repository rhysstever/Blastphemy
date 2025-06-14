using UnityEngine;

public class ImmolationFlame : MonoBehaviour
{
    private float damage, lifeSpan, currentLifeSpan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ImmolationAbility immolationAbility = AbilityManager.instance.GetAbility(Ability.Immolation) as ImmolationAbility;
        damage = immolationAbility.Damage;
        lifeSpan = immolationAbility.FlameLifeSpan;
        currentLifeSpan = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.CurrentMenuState == MenuState.Game) {
            currentLifeSpan += Time.deltaTime;

            if(currentLifeSpan >= lifeSpan) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Enemy") {
            collider.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
        }
    }
}
