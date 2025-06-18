using UnityEngine;

public class AbilityProjectile : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Vector2 velocity;
    protected float damage, lifeSpan, currentLifeSpan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLifeSpan = 0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(GameManager.instance.CurrentMenuState == MenuState.Game) {
            rb.linearVelocity = velocity;

            currentLifeSpan += Time.deltaTime;

            if(currentLifeSpan >= lifeSpan) {
                Destroy(gameObject);
            }
        } else {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void SetVelocity(Vector2 velocity) {
        this.velocity = velocity;
    }
}
