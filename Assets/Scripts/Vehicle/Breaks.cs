using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace LeglessDriving
{
    public class Breaks : IBrakes
    {
        private CarStats _breaks;
        private WheelCollider[] _wheelColliders;

        public void Initialize(CarStats stats, WheelCollider[] wheelColliders)
        {
            _breaks = stats;
            _wheelColliders = wheelColliders;
        }

        public void Break(float input)
        {
            _wheelColliders[0].brakeTorque =
            _wheelColliders[1].brakeTorque =
            _wheelColliders[2].brakeTorque =
            _wheelColliders[3].brakeTorque = input * _breaks.brakePower;
        }
    }
}
