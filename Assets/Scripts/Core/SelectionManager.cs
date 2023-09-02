using System;
using UnityEngine;
using Zenject;
using LeglessDriving;

public class SelectionManager : MonoBehaviour
{
    private IRaycast raycast;

    [Inject]
    private Player player;

    public static event Action<ISelectable> OnSelected;

    private void Awake()
    {
        raycast = GetComponent<IRaycast>();
    }

    private void Start()
    {
        InputManager.Instance.OnInteract += Instance_OnInteract;
        InputManager.Instance.OnGearDown += Instance_OnGearDown;
        InputManager.Instance.OnGearUp += Instance_OnGearUp;
    }

    private void Instance_OnGearUp()
    {
        raycast.GetRaycastResult()?.GetComponent<IShifter>()?.ShiftGear(1);
    }

    private void Instance_OnGearDown()
    {
        raycast.GetRaycastResult()?.GetComponent<IShifter>()?.ShiftGear(-1);
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnInteract -= Instance_OnInteract;
    }

    private void Instance_OnInteract()
    {
        raycast.GetRaycastResult()?.GetComponent<IInteractible>()?.Interact();
    }

    private void Update()
    {
        OnSelected?.Invoke(raycast.GetRaycastResult()?.GetComponent<ISelectable>());
    }
}
