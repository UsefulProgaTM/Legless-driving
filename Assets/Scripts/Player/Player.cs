using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class Player
    {
        public enum State
        {
            Driving,
            Pedaling,
        }

        public State state;

        private bool hasBrick = false;

        public bool HasBrick
        {
            get { return hasBrick; }
        }

        public void PutBrick()
        {
            hasBrick = false;
        }

        public void PickupBrick()
        {
            hasBrick = true;
        }
    }
}
