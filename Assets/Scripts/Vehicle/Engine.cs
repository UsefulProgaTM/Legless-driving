using UnityEngine;


namespace LeglessDriving
{
    public class Engine : IEngine
    {
        private IShifter _shifter;

        private WheelCollider[] _wheelColliders;

        private CarStats _carStats;

        public void Initialize(WheelCollider[] wheelColliders, CarStats stats, IShifter shifter)
        {
            _wheelColliders = wheelColliders;
            _carStats = stats;
            _shifter = shifter; 
        }

        public void Accelerate(float input, float rpm)
        {
            float reversingMultiplier = _shifter.IsReversing() ? -1 : 1;

            if (input < 0.2f)
                input = 0.1f;

            float forceToApply = input * reversingMultiplier * _carStats.horsePower.Evaluate(rpm);

            float decelerateForce = _wheelColliders[0].motorTorque * 0.99f;

            if (rpm > _carStats.maxRPM)
                forceToApply *= -1;

            if (_shifter.CheckIsClutchEngaged())
            {
                //clutch on, decelerate
                ApplyForcesToWheels(decelerateForce);
               // Debug.Log("clutch on, decelerate");
            }
            else
            {
                if (_shifter.IsInNeutral())
                {
                    //clutch off, in neutral, decelerate
                    ApplyForcesToWheels(decelerateForce);
                    //Debug.Log("clutch off, in neutral, decelerate");
                }
                else
                {
                    //clutch off, in gear, accelerate
                    if (input > 0.2f)
                    {
                        ApplyForcesToWheels(forceToApply);
                        //Debug.Log("clutch off, in gear, accelerate");
                    }
                    else
                    {
                        ApplyForcesToWheels(forceToApply);
                       // Debug.Log("clutch off, in gear, decelerate");
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
