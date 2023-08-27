using UnityEngine;

namespace LeglessDriving
{
    public interface IHandling
    {
        public void Initialize(WheelCollider[] wheelColliders, CarStats stats);
        public void SteerDriveWheels(float input);
    }
}
