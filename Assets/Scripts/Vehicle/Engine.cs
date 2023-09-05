using System.Collections.Generic;
using UnityEngine;


namespace LeglessDriving
{
    public class Engine : IEngine
    {
        private IShifter _shifter;

        private WheelCollider[] _wheelColliders;

        private CarStats _carStats;

        private Dictionary<int, float> _decelerateGearValueDictionary;

        public void Initialize(WheelCollider[] wheelColliders, CarStats stats, IShifter shifter)
        {
            _wheelColliders = wheelColliders;
            _carStats = stats;
            _shifter = shifter;

            _decelerateGearValueDictionary = new Dictionary<int, float>
            {
                {0, 0.001f},
                {1, 0.5f },
                {2, 0.99f},
                {3, 0.999f},
                {4, 0.9999f},
                {5, -0.5f },
            };
        }

        public void Accelerate(float input, float rpm)
        {
            float reversingMultiplier = _shifter.IsReversing() ? -1 : 1;

            if (input < 0.2f)
                input = 0.1f;


            if (_shifter.CheckIsClutchEngaged())
            {
                //clutch on, decelerate
                float decelerateForce = _wheelColliders[0].motorTorque * 0.999f;
                ApplyForcesToWheels(decelerateForce);
                //Debug.Log("clutch on, decelerate");
            }
            else
            {
                if (_shifter.IsInNeutral())
                {
                    //clutch off, in neutral, decelerate
                    float decelerateForce = _wheelColliders[0].motorTorque * 0.999f;
                    ApplyForcesToWheels(decelerateForce);
                    //Debug.Log("clutch off, in neutral, decelerate");
                }
                else
                {
                    //clutch off, in gear, accelerate
                    if (input > 0.2f)
                    {
                        float forceToApply = input * reversingMultiplier * _carStats.horsePower.Evaluate(rpm);

                        if (rpm > _carStats.maxRPM)
                            forceToApply = 0;

                        ApplyForcesToWheels(forceToApply);
                        //Debug.Log("clutch off, in gear, accelerate");
                    }
                    else
                    {
                        float forceToApply = _wheelColliders[0].motorTorque * _decelerateGearValueDictionary[_shifter.GetCurrentGearID()];
                        ApplyForcesToWheels(forceToApply);
                       //Debug.Log("clutch off, in gear, decelerate");
                    }
                }
            }
        }

        private void ApplyForcesToWheels(float force)
        {
            int size = _wheelColliders.Length;
            for (int i = 0; i < size; i++)
            {
                _wheelColliders[i].motorTorque = force;
            }
        }
    }
}
