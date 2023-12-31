using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace LeglessDriving
{
    [CreateAssetMenu()]
    public class CarStats : ScriptableObject
    {
        public AnimationCurve horsePower;
        public float[] gearRatios;
        public float finalDrive;
        public float minRPM;
        public float maxRPM;
        public float maxSpeed;
        public float downforce;
        public float brakePower;
        public float steerAngle;
        public float mass;
    }
}
