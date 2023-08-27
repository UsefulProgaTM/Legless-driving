using UnityEngine;

namespace LeglessDriving
{
    [CreateAssetMenu()]
    public class PlayerState : ScriptableObject
    {
        public State state;

        public enum State
        {
            Driving,
            Pedaling,
        }

        public bool HasBrick;

        public void PickupBrick()
        {
            HasBrick = true;
        }

        public void PutBrick()
        {
            HasBrick = false;
        }

        public void SetState(State state)
        {
            this.state = state;
        }

        public State GetState()
        {
            return state;
        }
    }
}
