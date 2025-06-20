using UnityEngine;

public class DynamicAbilityProjectile : AbilityProjectile
{
    [SerializeField]
    protected Rigidbody2D rb;
    protected Vector2 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        if(rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(GameManager.instance.CurrentMenuState == MenuState.Game) {
            rb.linearVelocity = velocity;
        } else {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void SetVelocity(Vector2 velocity) {
        this.velocity = velocity;
    }
}
