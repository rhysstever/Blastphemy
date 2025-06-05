using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private PlayerControls playerControls;
    [SerializeField]
    private GameObject playerPivot;

    private Vector2 shootDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        shootDirection = playerControls.GetShoot();
        UpdateCombat();
    }

    private void UpdateCombat() {
        playerPivot.transform.up = shootDirection;
    }
}
