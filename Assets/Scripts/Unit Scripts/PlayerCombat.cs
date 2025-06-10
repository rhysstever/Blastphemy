using UnityEngine;

public class PlayerCombat : UnitCombat
{
    [SerializeField]
    private PlayerControls playerControls;
    [SerializeField]
    private GameObject playerPivot, baseBulletPrefab;
    [SerializeField]
    private float fireRate;

    // Use shootDirection for the direction of any projectiles to be fired forward
    // All aimDirection variables are used for rotating the player
    private Vector2 shootDirection, aimDirectionCurrentFrame, aimDirection1FrameAgo, aimDirection2FramesAgo;
    private float fireTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start() {
        base.Start();

        aimDirectionCurrentFrame = Vector2.down;
        aimDirection1FrameAgo = aimDirectionCurrentFrame;
        aimDirection2FramesAgo = aimDirectionCurrentFrame;
        shootDirection = aimDirectionCurrentFrame;
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
            aimDirectionCurrentFrame = playerControls.GetShoot();
            // Aim with the previous frame's direction
            // The previous frame is used in case 2 keys are pressed at the same time to aim diagonally,
            // the player should only aim to the diagonal and not flicker to whichever key was pressed slightly before the other
            shootDirection = aimDirection1FrameAgo;
            // Set previous frame's direction as the previous previous frame's direction
            aimDirection2FramesAgo = aimDirection1FrameAgo;
            // Set new direction as new previous frame's direction
            aimDirection1FrameAgo = aimDirectionCurrentFrame;
        }
        // If the player is not aiming, but recent frame directions are different
        // Ex) If the player was aiming diagonally and release both keys near-simultaneously, there would be
        // a frame with the direction of only one key down (we want to ignore this frame in this case)
        else if(aimDirection1FrameAgo != aimDirection2FramesAgo) {
            // Aim with the previous previous frame's direction
            shootDirection = aimDirection2FramesAgo;
            // Set the previous frame's direction to the "new" (more like recovered) direction
            aimDirection1FrameAgo = aimDirection2FramesAgo;
        }

        // Rotate the player's aim to the proper direction
        playerPivot.transform.up = shootDirection;
    }

    private void Shoot() {
        if(fireTimer >= fireRate) {
            // Spawn a bullet in the aimed direction
            Vector2 newPosition = (Vector2)transform.position + shootDirection;
            GameObject bullet = Instantiate(baseBulletPrefab, newPosition, Quaternion.identity, GameManager.instance.BulletsParent);
            // Set the source and velocity of the bullet
            bullet.GetComponent<Bullet>().Source = this;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * bullet.GetComponent<Bullet>().ProjectileSpeed;
            // Reset the fire timer
            fireTimer = 0f;
        }
    }
}
