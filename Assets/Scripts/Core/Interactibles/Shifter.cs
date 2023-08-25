using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class Shifter : MonoBehaviour, IInteractible
    {
        public void Interact()
        {
            Debug.Log("shifter");
        }
    }
}
