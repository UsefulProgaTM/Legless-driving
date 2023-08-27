using UnityEngine;

namespace LeglessDriving
{
    public interface IBrakes
    {
        public void Initialize(CarStats stats, WheelCollider[] wheelColliders);
        public void Break(float input);
    }
}
