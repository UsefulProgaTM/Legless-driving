using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class StopTimerButton : MonoBehaviour, IInteractible
    {
        [Inject]
        private CarSoundManager _soundManager;

        [SerializeField]
        private SpeedometerUI speedometerUI;

        public void Interact()
        {
            speedometerUI.StopTimer();
            _soundManager.PlayButtonClickSound();

        }
    }
}
