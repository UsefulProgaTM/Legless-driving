using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    [CreateAssetMenu()]
    public class InteriorSoundSO : ScriptableObject
    {
        public AudioClip gearShiftSound;
        public AudioClip gearNeutralSound;
        public AudioClip gearMissedSound;
        public AudioClip handbrakePulledSound;
        public AudioClip handbrakeReleaseSound;
    }
}
