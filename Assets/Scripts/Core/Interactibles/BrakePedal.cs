using UnityEngine;

public class BrakePedal : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        Debug.Log("Brake");
    }
}
