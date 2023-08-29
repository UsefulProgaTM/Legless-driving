using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace LeglessDriving
{
    public class Transmission : ITransmission
    {
        private IShifter _shifter;

        private CarStats _carStats;

        private WheelCollider _wheelCollider;

            
        public void Initialize(CarStats carStats, WheelCollider wheelCollider, IShifter shifter)
        {
            _carStats = carStats;
            _wheelCollider = wheelCollider;
            _shifter = shifter;
        }

        public float EvaluateRPM()
        {
            float rpm = _wheelCollider.rpm * _wheelCollider.radius * _carStats.gearRatios[_shifter.GetGearID()] + _carStats.minRPM;
            
            return rpm;
        }
    }
}
