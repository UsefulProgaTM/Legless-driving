using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace LeglessDriving
{
    [CreateAssetMenu()]
    public class CarStats : ScriptableObject
    {
        public int HP;
        public float brakePower;
        public float steerAngle;
        public float mass;
    }
}
