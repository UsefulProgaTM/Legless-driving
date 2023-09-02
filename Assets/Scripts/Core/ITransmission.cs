using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public interface ITransmission
    {
        public void Initialize(CarStats carStats, WheelCollider[] wheelColliders, IShifter shifter);
        public float EvaluateRPM();
    }
}
