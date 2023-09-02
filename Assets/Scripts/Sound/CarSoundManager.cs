using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class CarSoundManager : MonoBehaviour
    {
        [SerializeField]
        private InteriorSoundSO _interiorSoundSO;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayGearShiftSound()
        {
            _audioSource.PlayOneShot(_interiorSoundSO.gearShiftSound);
        }
        public void PlayGearShiftNeutralSound()
        {
            _audioSource.PlayOneShot(_interiorSoundSO.gearNeutralSound);
        }
        public void PlayGearMissedSound()
        {
            _audioSource.PlayOneShot(_interiorSoundSO.gearMissedSound);
        }
        public void PlayHandbrakePulledSound()
        {
            _audioSource.PlayOneShot(_interiorSoundSO.handbrakePulledSound);
        }
        public void PlayHandbrakeReleasedSound()
        {
            _audioSource.PlayOneShot(_interiorSoundSO.handbrakeReleaseSound);
        }
    }
}
