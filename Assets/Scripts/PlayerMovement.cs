using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerControls playerControls;
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveAmount;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        moveAmount = playerControls.GetMove();
    }

    private void FixedUpdate() {
        Move();
    }

    private bool CanMove() {
        return true;
    }

    private void Move() {
        if(CanMove()) {
            rb.linearVelocity = new Vector2(moveAmount.x * moveSpeed, moveAmount.y * moveSpeed);
        }
    }
}

