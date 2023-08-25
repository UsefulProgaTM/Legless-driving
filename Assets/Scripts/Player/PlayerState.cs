using UnityEngine;

namespace LeglessDriving
{
    public class PlayerState : ScriptableObject
    {
        public State state;

        public enum State
        {
            Driving,
            Pedaling,
        }

        public void SetState(State state)
        {
            state = state;
        }

        public State GetState()
        {
            return state;
        }
    }
}
