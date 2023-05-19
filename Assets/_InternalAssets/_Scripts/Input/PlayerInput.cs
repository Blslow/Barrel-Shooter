using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private PlayerInputActions.PlayerMovementActions playerMovementActions;

    [Header("Events")]
    [SerializeField]
    private UnityEvent<Vector2> OnMovementInput;
    [SerializeField]
    private UnityEvent<Vector2> OnLookInput;
    [SerializeField]
    private UnityEvent OnJumpInput;
    [SerializeField]
    private UnityEvent OnCrouchInput;
    [SerializeField]
    private UnityEvent OnSprintInput;

    //[Header("Input Actions")]
    //[SerializeField]
    //private InputActionReference playerMove;
    //[SerializeField]
    //private InputActionReference jump;

    private void Awake()
    {
        playerInputActions = new();
        playerMovementActions = playerInputActions.PlayerMovement;
    }

    private void OnEnable()
    {
        playerMovementActions.Enable();
        playerMovementActions.Jump.performed += Jump;
        playerMovementActions.Crouch.performed += Crouch;
        playerMovementActions.Crouch.canceled += Crouch;
        playerMovementActions.Sprint.performed += Sprint;
        playerMovementActions.Sprint.canceled += Sprint;
    }

    private void OnDisable()
    {
        playerMovementActions.Disable();
        playerMovementActions.Jump.performed -= Jump;
        playerMovementActions.Crouch.performed -= Crouch;
        playerMovementActions.Crouch.canceled -= Crouch;
        playerMovementActions.Sprint.performed -= Sprint;
        playerMovementActions.Sprint.canceled -= Sprint;
    }

    private void Update()
    {
        OnMovementInput?.Invoke(playerMovementActions.Move.ReadValue<Vector2>().normalized);
    }

    private void LateUpdate()
    {
        OnLookInput?.Invoke(playerMovementActions.Look.ReadValue<Vector2>().normalized);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        OnJumpInput?.Invoke();
    }

    private void Crouch(InputAction.CallbackContext obj)
    {
        OnCrouchInput?.Invoke();
    }

    private void Sprint(InputAction.CallbackContext obj)
    {
        OnSprintInput?.Invoke();
    }
}
