using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public event Action OnHandbrakePerformed;

    public event Action OnChangePosition;
    public event Action OnInteract;
    public event Action OnGearDown;
    public event Action OnGearUp;


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
        playerInputActions.Player.ChangePosition.performed += ChangePosition_performed;
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.GearDown.performed += GearDown_performed;
        playerInputActions.Player.GearUp.performed += GearUp_performed;
    }

    private void GearUp_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGearUp?.Invoke();
    }

    private void GearDown_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGearDown?.Invoke();
    }

    public bool IsInteractPressed()
    {
        return playerInputActions.Player.Interact.IsPressed();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    private void ChangePosition_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnChangePosition?.Invoke();
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
