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
        return GameManager.instance.CurrentGameState == GameState.Game;
    }

    private void Move() {
        if(CanMove()) {
            rb.linearVelocity = moveSpeed * moveAmount;
        } else {
            rb.linearVelocity = Vector2.zero;
        }
    }
}

