using UnityEngine;

public class GasPedal : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        Debug.Log("gas");
    }
}
