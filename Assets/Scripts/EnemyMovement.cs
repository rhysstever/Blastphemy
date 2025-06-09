using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        Move();
    }

    private bool CanMove() {
        return true;
    }

    private void Move() {
        if(CanMove()) {
            // Get player's current position 
            Vector3 playerPos = GameManager.instance.GetPlayerPosition();
            Vector2 moveAmount = playerPos - transform.position;
            moveAmount = moveSpeed * Time.deltaTime * moveAmount.normalized;

            rb.linearVelocity = moveAmount;
        }
    }
}
