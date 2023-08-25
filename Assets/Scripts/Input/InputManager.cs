using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public event Action OnHandbrakePerformed;

    public event Action OnGoDown;
    public event Action OnGoUp;
    public event Action OnInteract;


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
        playerInputActions.Player.GoDown.performed += GoDown_performed;
        playerInputActions.Player.GoUp.performed += GoUp_performed;
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    private void GoUp_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGoUp?.Invoke();
    }

    private void GoDown_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGoDown?.Invoke();
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
