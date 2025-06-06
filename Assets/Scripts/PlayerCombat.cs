using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private PlayerControls playerControls;
    [SerializeField]
    private GameObject playerPivot;

    private Vector2 shootDirection;
    private Vector2 shootDirection1FrameAgo;
    private Vector2 shootDirection2FramesAgo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(playerControls.IsAiming()) {
            // Get new direction
            shootDirection = playerControls.GetShoot();
            // Aim with the previous frame's direction
            Aim(shootDirection1FrameAgo);
            // Set previous frame's direction as the previous previous frame's direction
            shootDirection2FramesAgo = shootDirection1FrameAgo;
            // Set new direction as new previous frame's direction
            shootDirection1FrameAgo = shootDirection;
        } 
        // If the player is not aiming, but recent frame directions are different
        // Ex) If the player was aiming diagonally and release both near-simultaneously, there would be
        // a frame in between with the direction of only one key down (we want to ignore this frame in this case)
        else if(shootDirection1FrameAgo != shootDirection2FramesAgo) {
            // Aim with the previous previous frame's direction
            Aim(shootDirection2FramesAgo);
            // Set the previous frame's direction to the "new" (more like recovered) direction
            shootDirection1FrameAgo = shootDirection2FramesAgo;
        }
    }

    public void Aim(Vector2 aimDirection) {
        playerPivot.transform.up = aimDirection;
    }
}
