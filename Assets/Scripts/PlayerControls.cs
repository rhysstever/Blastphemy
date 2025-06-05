using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset InputActions;

    private InputAction moveAction, shootAction;

    private void OnEnable() {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable() {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake() {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    public Vector2 GetMove() {
        return moveAction.ReadValue<Vector2>();
    }

    public Vector2 GetShoot() {
        return shootAction.ReadValue<Vector2>();
    }
}
