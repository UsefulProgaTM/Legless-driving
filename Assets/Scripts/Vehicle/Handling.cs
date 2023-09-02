using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class Handling: IHandling
    {
        private WheelCollider[] _wheelColliders;
        private CarStats _stats;

        private float smDampVelocity;
        private float smDampTime = 0.05f;

        public void Initialize(WheelCollider[] wheelColliders, CarStats stats)
        {
            _wheelColliders = wheelColliders;
            _stats = stats;

            
        }

        public void SteerDriveWheels(float input)
        {
            float targetAngle = input * _stats.steerAngle;
            float angle = Mathf.SmoothDamp(_wheelColliders[0].steerAngle, targetAngle, ref smDampVelocity, smDampTime);
            _wheelColliders[0].steerAngle =
            _wheelColliders[1].steerAngle = angle;
        }
    }
}
