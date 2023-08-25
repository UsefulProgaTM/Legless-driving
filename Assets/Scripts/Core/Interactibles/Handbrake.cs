using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class Handbrake : MonoBehaviour, IInteractible
    {
        public void Interact()
        {
            Debug.Log("Handbrake");
        }
    }
}
