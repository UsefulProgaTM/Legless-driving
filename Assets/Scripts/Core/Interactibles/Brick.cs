using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class Brick : MonoBehaviour, IInteractible
    {
        public void Interact()
        {
            Debug.Log("Brick");
        }
    }
}
