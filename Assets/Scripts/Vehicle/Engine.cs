using UnityEngine;


namespace LeglessDriving
{
    public class Engine : IEngine
    {
        private IShifter _shifter;

        private WheelCollider[] _wheelColliders;

        private CarStats _engineStats;

        public void Initialize(WheelCollider[] wheelColliders, CarStats stats, IShifter shifter)
        {
            _wheelColliders = wheelColliders;
            _engineStats = stats;
            _shifter = shifter; 
        }

        public void Accelerate(float input)
        {
            float reversingMultiplier = _shifter.IsReversing() ? -1 : 1;

            if(_shifter.IsInNeutral())
            {
                _wheelColliders[0].motorTorque =
                _wheelColliders[1].motorTorque =
                _wheelColliders[2].motorTorque =
                _wheelColliders[3].motorTorque = 0;
                return;
            }

            if(_shifter.CheckIsClutchEngaged())
            {
                _wheelColliders[0].motorTorque =
                _wheelColliders[1].motorTorque =
                _wheelColliders[2].motorTorque =
                _wheelColliders[3].motorTorque = 0;
            }
            else
            {
                _wheelColliders[0].motorTorque =
                _wheelColliders[1].motorTorque =
                _wheelColliders[2].motorTorque =
                _wheelColliders[3].motorTorque = input * reversingMultiplier;
            }
        }
    }
}
