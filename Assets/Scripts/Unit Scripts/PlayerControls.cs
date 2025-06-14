using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset InputActions;

    private InputAction moveAction, shootAction, pauseAction;

    private void Awake() {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void OnEnable() {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable() {
        InputActions.FindActionMap("Player").Disable();
    }

    public Vector2 GetMove() {
        return moveAction.ReadValue<Vector2>();
    }

    public bool IsAiming() {
        return shootAction.phase != InputActionPhase.Waiting;
    }

    public Vector2 GetShoot() {
        return shootAction.ReadValue<Vector2>();
    }

    public bool IsPauseClicked() {
        return pauseAction.triggered;
    }
}
