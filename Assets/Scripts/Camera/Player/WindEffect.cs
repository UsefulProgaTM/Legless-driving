using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace LeglessDriving
{
    public class WindEffect : MonoBehaviour
    {
        [SerializeField]
        private VisualEffect[] windEffects;

        [SerializeField]
        private Rigidbody rb;

        [Inject]
        private CurrentCarStats currentCarStats;

        private const string SPEED_VALUE = "Speed";
        private const string VELOCITY_VALUE = "CarVelocity";

        private void FixedUpdate()
        {
            float speed = currentCarStats.speed;

            Vector3 velocity =  transform.InverseTransformDirection(rb.velocity);

            windEffects[0].SetFloat(SPEED_VALUE, speed);
            windEffects[1].SetFloat(SPEED_VALUE, speed);
            windEffects[0].SetVector3(VELOCITY_VALUE, velocity);
            windEffects[1].SetVector3(VELOCITY_VALUE, velocity);
        }
    }
}
