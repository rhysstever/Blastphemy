using UnityEngine;

public class BrimstoneChunk : MonoBehaviour
{
    private float damage, lifeSpan, currentLifeSpan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BrimstoneAbility brimstoneAbility = AbilityManager.instance.GetAbility(Ability.Brimstone) as BrimstoneAbility;
        damage = brimstoneAbility.Damage;
        lifeSpan = brimstoneAbility.ChunkLifeSpan;
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
