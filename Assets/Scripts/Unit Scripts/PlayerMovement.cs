using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerControls playerControls;
    [SerializeField]
    private GameObject playerPivot;
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveAmount, previousMoveAmount;

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
        return GameManager.instance.CurrentMenuState == MenuState.Game;
    }

    private void Move() {
        if(CanMove()) {
            rb.linearVelocity = moveSpeed * moveAmount;

            playerPivot.transform.up = GetAimDirection();
        } else {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public Vector2 GetAimDirection() {
        if(moveAmount == Vector2.zero) {
            return previousMoveAmount;
        } else {
            previousMoveAmount = moveAmount.normalized;
            return moveAmount.normalized;
        }
    }
}

