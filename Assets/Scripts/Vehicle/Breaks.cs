using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace LeglessDriving
{
    public class Breaks : MonoBehaviour
    {
        [SerializeField]
        private BrakePedal _brakePedal;

        [SerializeField]
        private Handbrake _handbrake;

        private CarStats _breaks;

        private Rigidbody _rb;

        private WheelCollider[] _wheelColliders;

        public void Initialize(Rigidbody rb, CarStats stats, WheelCollider[] wheelColliders)
        {
            this._rb = rb;
            _breaks = stats;
            _wheelColliders = wheelColliders;
        }

        private void FixedUpdate()
        {
            Break(_brakePedal.GetInput());
            Handbrake(_handbrake.GetInput());
        }

        private void Break(float input)
        {
            float localVelocityZ = transform.InverseTransformDirection(_rb.velocity).z;
            if (localVelocityZ > 0)
            {
                if (input > 0)
                {
                    _wheelColliders[0].brakeTorque =
                    _wheelColliders[1].brakeTorque =
                    _wheelColliders[2].brakeTorque =
                    _wheelColliders[3].brakeTorque = input * _breaks.brakePower;
                }
                else
                {
                    RemoveBreakForce();
                }
            }
        }

        private void RemoveBreakForce()
        {
            _wheelColliders[0].brakeTorque =
                    _wheelColliders[1].brakeTorque =
                    _wheelColliders[2].brakeTorque =
                    _wheelColliders[3].brakeTorque = 0;
        }

        private void Handbrake(bool enabled)
        {
            if(enabled)
            {
                    _wheelColliders[2].brakeTorque =
                    _wheelColliders[3].brakeTorque = 5 * _breaks.brakePower;
            }
            else
            {
                    _wheelColliders[2].brakeTorque =
                    _wheelColliders[3].brakeTorque = 0;
            }
        }
    }
}
