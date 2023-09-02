using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class TiresOnRoadSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        [Inject]
        private CurrentCarStats _currentCarStats;

        private void FixedUpdate()
        {
                _audioSource.volume = Mathf.Clamp01(_currentCarStats.speed / _currentCarStats.maxSpeed);
        }
    }
}
