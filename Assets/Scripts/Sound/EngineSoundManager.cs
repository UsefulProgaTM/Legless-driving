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
        [Inject]
        private CurrentCarStats _currentCarStats;
        private CarStats _carStats;

        private AudioSource _fadeInAudioSource;
        private AudioSource _fadeOutAudioSource;

        [SerializeField]
        private AudioSource _audioSource1;
        [SerializeField]
        private AudioSource _audioSource2;

        [SerializeField] 
        private AudioSource _revLimitAudioSource;

        [SerializeField]
        private AudioClip _revLimitSound;
        private float _revLimitAudioDuration = 0.325f;
        private float revLimitlastPlayedTime;


        [SerializeField]
        private AudioClip[] _audioClips;

        private float[] RPMThresholds;


        public int currentRPMPartID;
        public int nextRPMPart;

        private float crossFadeOffset = 0.2f;

        public void Initialize(ITransmission transmission, CarStats carStats)
        {
            _fadeOutAudioSource = _audioSource1;
            _fadeInAudioSource = _audioSource2;

            _fadeInAudioSource.Play();
            _fadeOutAudioSource.Play();

            _transmission = transmission;
            _carStats = carStats;

            RPMThresholds = new float[] {0.05f, 0.25f, 0.5f, 0.75f };
            currentRPMPartID = 0;
            nextRPMPart = 1;

            revLimitlastPlayedTime = Time.time;
        }

        public bool _accelerating = false;
        public float lastRPMPercent;
        public float rpmPercent;

        public void ManageEngineSound()
        {
            lastRPMPercent = rpmPercent;

            rpmPercent = _currentCarStats.rpm / _carStats.maxRPM - _carStats.minRPM / _carStats.maxRPM;

            rpmPercent = Mathf.Clamp01(rpmPercent);

            PlayRevLimitSound(rpmPercent);

            _audioSource1.volume = 1 - rpmPercent * 9f;
            _audioSource2.volume = Mathf.Clamp01(rpmPercent * 5f);
            _audioSource2.pitch = rpmPercent / 2 + 0.5f;

            _accelerating = rpmPercent > lastRPMPercent;
        }

        private void PlayRevLimitSound(float percent)
        {
            if(percent + _carStats.minRPM / _carStats.maxRPM > 0.99f && Time.time >= revLimitlastPlayedTime + _revLimitAudioDuration)
            {
                revLimitlastPlayedTime = Time.time;
                _revLimitAudioSource.PlayOneShot(_revLimitSound);
            }
        }

        private void FixedUpdate()
        {
                     
            //nextRPMPart = GetNextRPMPartID(rpmPercent);
            //currentRPMPartID = ChangeCurrentRPMID(rpmPercent);
            //CrossFade(rpmPercent);
        }


        //private int GetNextRPMPartID(float percent)
        //{
        //    if(_accelerating)
        //    {
        //        if (currentRPMPartID + 1 >= RPMThresholds.Length)
        //            return RPMThresholds.Length - 1;
                
        //        if (percent > RPMThresholds[currentRPMPartID + 1] - crossFadeOffset)
        //            return currentRPMPartID + 1;
        //        else
        //            return currentRPMPartID;
        //    }
        //    else
        //    {
        //        if (currentRPMPartID - 1 < 0)
        //            return 0;
                
        //        if (percent < RPMThresholds[currentRPMPartID - 1] + crossFadeOffset)
        //            return currentRPMPartID - 1;
        //        return currentRPMPartID;
        //    }
        //}
        //private int ChangeCurrentRPMID(float percent)
        //{
        //    if(_accelerating)
        //    {
        //        if(currentRPMPartID + 1 >= RPMThresholds.Length)
        //            return currentRPMPartID;

        //        if (percent > RPMThresholds[currentRPMPartID + 1])
        //        {
        //            _fadeOutAudioSource = _fadeInAudioSource;
        //            _fadeInAudioSource = _fadeInAudioSource == _audioSource1 ? _audioSource2 : _audioSource1;

        //            _fadeOutAudioSource.clip = _audioClips[currentRPMPartID];
        //            _fadeInAudioSource.clip = _audioClips[nextRPMPart];

        //            _fadeOutAudioSource.Play();
        //            _fadeInAudioSource.Play();

        //            return currentRPMPartID + 1;
        //        }
        //        return currentRPMPartID;
        //    }
        //    else
        //    {
        //        if (currentRPMPartID - 1 < 0)
        //            return currentRPMPartID;

        //        if (percent < RPMThresholds[currentRPMPartID - 1])
        //        {
        //            _fadeOutAudioSource = _fadeInAudioSource;
        //            _fadeInAudioSource = _fadeInAudioSource == _audioSource1 ? _audioSource2 : _audioSource1;

        //            _fadeOutAudioSource.clip = _audioClips[currentRPMPartID];
        //            _fadeInAudioSource.clip = _audioClips[nextRPMPart];

        //            _fadeOutAudioSource.Play();
        //            _fadeInAudioSource.Play();
        //            return currentRPMPartID - 1;
        //        }
        //        return currentRPMPartID;
        //    }
        //}

        //private void CrossFade(float percent)
        //{
        //    _fadeOutAudioSource.volume = 1 - percent / RPMThresholds[currentRPMPartID];
        //    _fadeInAudioSource.volume = percent / RPMThresholds[nextRPMPart];
        //}
    }
}
