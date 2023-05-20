using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private PlayerInputActions.PlayerActions playerActions;

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
    [SerializeField]
    private UnityEvent OnStartShootingInput;
    [SerializeField]
    private UnityEvent OnStopShootingInput;
    [SerializeField]
    private UnityEvent<float> OnMouseScroll;

    //[Header("Input Actions")]
    //[SerializeField]
    //private InputActionReference playerMove;
    //[SerializeField]
    //private InputActionReference jump;

    private void Awake()
    {
        playerInputActions = new();
        playerActions = playerInputActions.Player;
    }

    private void OnEnable()
    {
        playerActions.Enable();
        playerActions.Jump.performed += Jump;
        playerActions.Crouch.performed += Crouch;
        playerActions.Crouch.canceled += Crouch;
        playerActions.Sprint.performed += Sprint;
        playerActions.Sprint.canceled += Sprint;
        playerActions.Shoot.performed += StartShooting;
        playerActions.Shoot.canceled += StopShooting;
        playerActions.MouseScrollY.performed += SwitchWeapon;
    }

    private void OnDisable()
    {
        playerActions.Disable();
        playerActions.Jump.performed -= Jump;
        playerActions.Crouch.performed -= Crouch;
        playerActions.Crouch.canceled -= Crouch;
        playerActions.Sprint.performed -= Sprint;
        playerActions.Sprint.canceled -= Sprint;
        playerActions.Shoot.performed -= StartShooting;
        playerActions.Shoot.canceled -= StopShooting;
        playerActions.MouseScrollY.performed -= SwitchWeapon;
    }

    private void Update()
    {
        OnMovementInput?.Invoke(playerActions.Move.ReadValue<Vector2>().normalized);
    }

    private void LateUpdate()
    {
        OnLookInput?.Invoke(playerActions.Look.ReadValue<Vector2>().normalized);
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

    private void StartShooting(InputAction.CallbackContext obj)
    {
        OnStartShootingInput?.Invoke();
    }

    private void StopShooting(InputAction.CallbackContext obj)
    {
        OnStopShootingInput?.Invoke();
    }

    private void SwitchWeapon(InputAction.CallbackContext obj)
    {
        OnMouseScroll?.Invoke(obj.ReadValue<float>());
    }
}
