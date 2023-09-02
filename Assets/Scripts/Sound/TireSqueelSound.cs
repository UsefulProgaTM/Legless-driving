using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class TireSqueelSound : MonoBehaviour
    {
        [SerializeField]
        private WheelCollider[] _wheels;

        private int size = 0;

        [SerializeField]
        private AudioSource _audioSource;

        private float smDampVelocity;
        private float smDampTime = 0.33f;

        private void Awake()
        {
            size = _wheels.Length;
        }

        private void FixedUpdate()
        {
            float targetVolume = 0;
            for(int i = 0; i < size;i++)
            {
                if(_wheels[i].GetGroundHit(out var hit))
                {
                    float compare = (Mathf.Abs(hit.sidewaysSlip));
                    if (compare > targetVolume)
                        targetVolume = compare;
                }
            }
            targetVolume -= 0.2f;
            _audioSource.volume = Mathf.SmoothDamp(_audioSource.volume, targetVolume, ref smDampVelocity, smDampTime);
        }
    }
}
