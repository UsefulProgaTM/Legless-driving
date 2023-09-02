using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace LeglessDriving
{
    public class Transmission : ITransmission
    {
        private IShifter _shifter;

        private CarStats _carStats;

        private WheelCollider[] _driveWheels;

        public void Initialize(CarStats carStats, WheelCollider[] driveWheels, IShifter shifter)
        {
            _carStats = carStats;
            _driveWheels = driveWheels;
            _shifter = shifter;
        }

        public float EvaluateRPM()
        {
            float wheelsRpm = 0;
            for(int i = 0;i < _driveWheels.Length;i++)
            {
                wheelsRpm += _driveWheels[i].rpm;
            }
            float rpm = Mathf.Abs(wheelsRpm) / _driveWheels.Length * _driveWheels[0].radius * _carStats.gearRatios[_shifter.GetGearPositionID()] * _carStats.finalDrive + _carStats.minRPM;
            
            return rpm;
        }
    }
}
