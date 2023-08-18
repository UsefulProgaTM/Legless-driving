using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public event Action OnHandbrakePerformed;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Handbrake.performed += Handbrake_performed;
    }

    private void Handbrake_performed(InputAction.CallbackContext obj)
    {
        OnHandbrakePerformed?.Invoke();    
    }


    public Vector2 GetMovementVector()
    {
        return playerInputActions.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMousePositionDeltaVector()
    {
        return playerInputActions.Player.Look.ReadValue<Vector2>();
    }
}
