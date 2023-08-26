using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class Handling : MonoBehaviour
    {
        private WheelCollider[] _wheelColliders;

        private CarStats _stats;

        [Inject]
        private IHorizontalInput input;

        public void Initialize(WheelCollider[] wheelColliders, CarStats stats)
        {
            _wheelColliders = wheelColliders;
            _stats = stats;
        }

        private void FixedUpdate()
        {
            SteerDriveWheels(input.GetHorizontalInput());
        }
        
        private void SteerDriveWheels(float input)
        {
            _wheelColliders[0].steerAngle = input * _stats.steerAngle;
            _wheelColliders[1].steerAngle = input * _stats.steerAngle;
        }
    }
}
