using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class Handling: IHandling
    {
        private WheelCollider[] _wheelColliders;
        private CarStats _stats;

        public void Initialize(WheelCollider[] wheelColliders, CarStats stats)
        {
            _wheelColliders = wheelColliders;
            _stats = stats;
        }

        public void SteerDriveWheels(float input)
        {
            _wheelColliders[0].steerAngle = input * _stats.steerAngle;
            _wheelColliders[1].steerAngle = input * _stats.steerAngle;
        }
    }
}
