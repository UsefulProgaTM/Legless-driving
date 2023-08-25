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
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnInteract -= Instance_OnInteract;
    }

    private void Instance_OnInteract()
    {
        raycast.GetRaycastResult()?.GetComponent<IInteractible>()?.Interact(player.HasBrick);
    }

    private void Update()
    {
        OnSelected?.Invoke(raycast.GetRaycastResult()?.GetComponent<ISelectable>());
    }
}
