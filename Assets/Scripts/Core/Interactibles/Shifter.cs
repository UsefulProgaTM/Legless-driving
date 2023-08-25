using UnityEngine;

namespace LeglessDriving
{
    public class Shifter : MonoBehaviour, IInteractible
    {
        public void Interact(bool hasBrick)
        {
            Debug.Log("shifter");
        }
    }
}
