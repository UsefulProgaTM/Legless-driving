using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class PlayerHandbrakeInput : IInteractible, IHandbrakeInput
    {
        public void Interact()
        {

        }

        public bool GetInput()
        {
            return true;
        }
    }
}
