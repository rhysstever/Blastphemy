using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField]
    private float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
        }
    }
}
