using UnityEngine;

public class EnemyCombat : UnitCombat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);

        if(currentHealth <= 0f) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
        }
    }
}
