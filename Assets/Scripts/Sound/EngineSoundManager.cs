using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class EngineSoundManager : MonoBehaviour
    {
        private ITransmission _transmission;
        private CarStats _carStats;

        [SerializeField]
        private AudioSource _audioSourceIdle;

        [SerializeField]
        private AudioSource _audioSourceLowRPM;
        [SerializeField]
        private AudioSource _audioSourceHighRPM;


        [SerializeField]
        private AudioClip _idleClip;
        [SerializeField]
        private AudioClip _accelerateClip;

        private float idlePitchThreshold;
        private float lowPitchThreshold;

        public void Initialize(ITransmission transmission, CarStats carStats)
        {
            _transmission = transmission;
            _carStats = carStats;

            idlePitchThreshold = (_carStats.minRPM * 1.1f) / _carStats.maxRPM;
            lowPitchThreshold = (_carStats.maxRPM / 2 + _carStats.minRPM) / _carStats.maxRPM;
        }

        // Update is called once per frame
        void Update()
        {
            float pitch = _transmission.EvaluateRPM() / _carStats.maxRPM;
            HandleIdleSound(pitch);
            HandleLowRPMSound(pitch);
            HandleHighRPMSound(pitch);
        }

        private void HandleIdleSound(float pitch)
        {
            float soundModifier  = pitch / idlePitchThreshold;
            _audioSourceIdle.volume = soundModifier;
            _audioSourceIdle.pitch = soundModifier;
        }

        private void HandleLowRPMSound(float pitch)
        {
            float soundModifier = (pitch / lowPitchThreshold) - idlePitchThreshold;

            _audioSourceLowRPM.volume = soundModifier;
            _audioSourceLowRPM.pitch = soundModifier;
        }

        private void HandleHighRPMSound(float pitch)
        {
            float soundModifier = pitch  - idlePitchThreshold;

            _audioSourceHighRPM.volume = soundModifier;
            _audioSourceHighRPM.pitch = soundModifier;
        }
    }
}
