using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private IRaycast raycast;

    public static event Action<ISelectable> OnSelected;

    private void Awake()
    {
        raycast = GetComponent<IRaycast>();
    }


    private void Update()
    {
        OnSelected?.Invoke(raycast.GetRaycastResult()?.GetComponent<ISelectable>());
        if(Input.GetKeyDown(KeyCode.F)) 
        {
            raycast.GetRaycastResult().GetComponent<IInteractible>().Interact();
        }
    }
}
