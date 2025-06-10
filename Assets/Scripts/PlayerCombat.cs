using UnityEngine;

public class PlayerCombat : UnitCombat
{
    [SerializeField]
    private PlayerControls playerControls;
    [SerializeField]
    private GameObject playerPivot, baseBulletPrefab;
    [SerializeField]
    private float fireRate;

    private Vector2 shootDirection, shootDirection1FrameAgo, shootDirection2FramesAgo;
    private float fireTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start() {
        base.Start();

        shootDirection = Vector2.down;
        shootDirection1FrameAgo = Vector2.down;
        shootDirection2FramesAgo = Vector2.down;
    }

    // Update is called once per frame
    void Update() {
        Aim();
        Shoot();
        fireTimer += Time.deltaTime;
    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);

        if(currentHealth <= 0f) {
            GameManager.instance.ChangeGameState(GameState.End);
        }
    }

    private void Aim() {
        if(playerControls.IsAiming()) {
            // Get new direction
            shootDirection = playerControls.GetShoot();
            // Aim with the previous frame's direction
            playerPivot.transform.up = shootDirection1FrameAgo;
            // Set previous frame's direction as the previous previous frame's direction
            shootDirection2FramesAgo = shootDirection1FrameAgo;
            // Set new direction as new previous frame's direction
            shootDirection1FrameAgo = shootDirection;
        }
        // If the player is not aiming, but recent frame directions are different
        // Ex) If the player was aiming diagonally and release both keys near-simultaneously, there would be
        // a frame with the direction of only one key down (we want to ignore this frame in this case)
        else if(shootDirection1FrameAgo != shootDirection2FramesAgo) {
            // Aim with the previous previous frame's direction
            playerPivot.transform.up = shootDirection2FramesAgo;
            // Set the previous frame's direction to the "new" (more like recovered) direction
            shootDirection1FrameAgo = shootDirection2FramesAgo;
        }
    }

    private void Shoot() {
        if(fireTimer >= fireRate) {
            // Spawn a bullet in the aimed direction
            Vector2 newPosition = (Vector2)transform.position + shootDirection;
            GameObject bullet = Instantiate(baseBulletPrefab, newPosition, Quaternion.identity);
            // Set the source and velocity of the bullet
            bullet.GetComponent<Bullet>().Source = this;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * bullet.GetComponent<Bullet>().ProjectileSpeed;
            // Reset the fire timer
            fireTimer = 0f;
        }
    }
}
