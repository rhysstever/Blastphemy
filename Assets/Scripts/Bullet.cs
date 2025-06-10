using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;

    private UnitCombat source;

    public float ProjectileSpeed { get { return projectileSpeed; } }
    public UnitCombat Source { set { source = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if((gameObject.CompareTag("PlayerBullet") && collision.gameObject.CompareTag("Enemy"))
            || (gameObject.CompareTag("EnemyBullet") && collision.gameObject.CompareTag("Player"))) {
            collision.gameObject.GetComponent<UnitCombat>().TakeDamage(source.Damage);
            Destroy(gameObject);
        }
    }
}
