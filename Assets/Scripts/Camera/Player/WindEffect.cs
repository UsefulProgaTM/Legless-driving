using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace LeglessDriving
{
    public class WindEffect : MonoBehaviour
    {
        [SerializeField]
        private VisualEffect[] windEffects;

        [Inject]
        private CurrentCarStats currentCarStats;

        private const string SPEED_VALUE = "Speed";

        private void FixedUpdate()
        {
            float speed = currentCarStats.speed;
            if(speed < 7)
                speed = 0;
            windEffects[0].SetFloat(SPEED_VALUE, speed);
            windEffects[1].SetFloat(SPEED_VALUE, speed);
        }
    }
}
