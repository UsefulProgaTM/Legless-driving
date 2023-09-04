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

        private float revvingRPM = 0;
        private float timeToHitMaxRPM = 2;
        private float unrevvingRPM = 0;
        private float timeToHitMinRPM = 5;

        private float smDampVelocity;
        private float smDampSpeed = 2;

        private float rpm = 0;

        public void Initialize(CarStats carStats, WheelCollider[] driveWheels, IShifter shifter)
        {
            _carStats = carStats;
            _driveWheels = driveWheels;
            _shifter = shifter;
        }

        public float EvaluateRPM(float input)
        {
            if(_shifter.IsInNeutral() || _shifter.CheckIsClutchEngaged())
            {
                if (input > 0.2f)
                {
                    rpm = CalculateRevvingRPM();
                }
                else
                {
                    rpm = CalculateUnrevvingRPM();
                }
            }
            else
            {
                float targetRPM = CalculateRPM();
                if (Mathf.Abs(targetRPM - rpm) > 300)
                    rpm = Mathf.SmoothDamp(rpm, targetRPM, ref smDampVelocity, smDampSpeed);
                else
                    rpm = targetRPM;
            }       
            return rpm;
        }

        private float CalculateRevvingRPM()
        {
            revvingRPM = rpm + _carStats.maxRPM * Time.deltaTime / timeToHitMaxRPM;

            if(revvingRPM > _carStats.maxRPM)
                revvingRPM = _carStats.maxRPM - UnityEngine.Random.Range(150,300);

            return revvingRPM;
        }

        private float CalculateUnrevvingRPM()
        {
            unrevvingRPM = rpm - _carStats.maxRPM * Time.deltaTime / timeToHitMinRPM;

            if (unrevvingRPM < _carStats.minRPM)
                unrevvingRPM = _carStats.minRPM;

            return unrevvingRPM;
        }

        private float CalculateRPM()
        {
            float wheelRPM = 0;
            for (int i = 0; i < _driveWheels.Length; i++)
            {
                wheelRPM += _driveWheels[i].rpm;
            }
            return Mathf.Abs(wheelRPM) / _driveWheels.Length * _driveWheels[0].radius * _carStats.gearRatios[_shifter.GetGearPositionID()] * _carStats.finalDrive + _carStats.minRPM;
        }
    }
}
