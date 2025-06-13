using UnityEngine;

public class EnemyCombat : UnitCombat
{
    [SerializeField]
    private float damage, attackRate;

    private float currentAttackTimer;

    public float Damage { get { return damage; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        currentAttackTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.CurrentGameState == GameState.Game) {
            currentAttackTimer += Time.deltaTime;
        }
    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);

        if(currentHealth <= 0f) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            if(currentAttackTimer >= attackRate) {
                collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
                currentAttackTimer = 0f;
            }
        }
    }
}
