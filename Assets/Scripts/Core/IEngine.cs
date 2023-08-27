using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public interface IEngine
    {
        public void Initialize(WheelCollider[] wheelColliders, CarStats stats, IShifter shifter);
        public void Accelerate(float input, float rpm);
    }
}
