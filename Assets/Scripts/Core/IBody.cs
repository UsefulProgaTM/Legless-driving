using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public interface IBody
    {
        public void Initialize(Rigidbody rb, CarStats stats, Transform transform, Transform centerOfMass);
        public void AddDownforce();
    }
}
