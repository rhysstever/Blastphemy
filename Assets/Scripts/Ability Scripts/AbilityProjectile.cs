using UnityEngine;

public class AbilityProjectile : MonoBehaviour
{
    private float damage, lifeSpan, currentLifeSpan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BrimstoneAbility ability = GameManager.instance.PlayerObject.GetComponent<BrimstoneAbility>();
        damage = ability.Damage;
        lifeSpan = ability.ChunkLifeSpan;
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
