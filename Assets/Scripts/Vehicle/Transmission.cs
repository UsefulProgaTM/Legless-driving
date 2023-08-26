using UnityEngine;
using UnityEngine.Rendering;

namespace LeglessDriving
{
    public class Transmission : MonoBehaviour
    {
        private Shifter _shifter;

        private CarStats _carStats;

        private WheelCollider _wheelCollider;
            
        public void Initialize(CarStats carStats, WheelCollider wheelCollider, Shifter shifter)
        {
            _carStats = carStats;
            _wheelCollider = wheelCollider;
            _shifter = shifter;
        }

        public float EvaluateRPM()
        {
            float rpm = _wheelCollider.rpm * _carStats.gearRatios[_shifter.GetGearId()] + _carStats.minRPM;

            return rpm > _carStats.maxRPM ?_carStats.maxRPM : rpm;
        }

        public bool CheckClutchEngaged()
        {
            return _shifter.CheckIsClutchEngaged();
        }
    }
}
