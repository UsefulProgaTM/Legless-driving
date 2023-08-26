using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace LeglessDriving
{
    public class Engine : MonoBehaviour
    {
        [SerializeField]
        private GasPedal _gasPedal;

        private Shifter _shifter;

        private WheelCollider[] _wheelColliders;

        private CarStats _engineStats;
        private Transmission _transmission;

        private float rpm;

        public void Initialize(WheelCollider[] wheelColliders, CarStats stats, Transmission transmission, Shifter shifter)
        {
            _wheelColliders = wheelColliders;
            _engineStats = stats;
            _transmission = transmission;
            _shifter = shifter; 
        }

        private void FixedUpdate()
        {
            Accelerate(_gasPedal.GetInput());
        }

        private void Accelerate(float input)
        {
            float reversingMultiplier = _shifter.IsReversing() ? -1 : 1;

            if(_shifter.IsInNeutral())
            {
                _wheelColliders[0].motorTorque =
                _wheelColliders[1].motorTorque =
                _wheelColliders[2].motorTorque =
                _wheelColliders[3].motorTorque = 0;
                return;
            }

            if(_shifter.CheckIsClutchEngaged())
            {
                _wheelColliders[0].motorTorque =
                _wheelColliders[1].motorTorque =
                _wheelColliders[2].motorTorque =
                _wheelColliders[3].motorTorque = 0;
            }
            else
            {
                _wheelColliders[0].motorTorque =
                _wheelColliders[1].motorTorque =
                _wheelColliders[2].motorTorque =
                _wheelColliders[3].motorTorque = input * reversingMultiplier * _engineStats.horsePower.Evaluate(_transmission.EvaluateRPM());
            }

            Debug.Log((_engineStats.horsePower.Evaluate(_transmission.EvaluateRPM()) + " " + _transmission.EvaluateRPM()));
        }
    }
}
