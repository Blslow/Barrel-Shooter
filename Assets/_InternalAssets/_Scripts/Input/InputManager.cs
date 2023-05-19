using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private PlayerInputActions.PlayerMovementActions playerMovementActions;

    private void Awake()
    {
        playerInputActions = new();
        playerMovementActions = playerInputActions.PlayerMovement;
    }

    private void OnEnable()
    {
        playerMovementActions.Enable();
    }

    private void OnDisable()
    {
        playerMovementActions.Disable();
    }
}
