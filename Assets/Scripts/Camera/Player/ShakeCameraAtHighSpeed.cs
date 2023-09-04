using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class ShakeCameraAtHighSpeed : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

        [Inject]
        private CurrentCarStats _currentCarStats;

        private float defaultShakeAmp = 0;
        private float defaultShakeFreq = 0;

        private float maxShakeAmp = 1f;
        private float maxShakeFreq = 4f;

        private void Awake()
        {
            _multiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        // Update is called once per frame
        void Update()
        {
            float value = _currentCarStats.speed / _currentCarStats.maxSpeed;
            _multiChannelPerlin.m_FrequencyGain = value * maxShakeFreq;
            _multiChannelPerlin.m_AmplitudeGain = value * maxShakeAmp;
        }
    }
}
