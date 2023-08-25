using UnityEngine;

namespace LeglessDriving
{
    public class Handbrake : MonoBehaviour, IInteractible
    {
        public void Interact(bool hasBrick)
        {
            Debug.Log("Handbrake");
        }
    }
}
