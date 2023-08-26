using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class Body : MonoBehaviour
    {
        private Rigidbody _rb;
        private CarStats _stats;

        [SerializeField]
        private Transform centerOfMass;

        public void Initialize(Rigidbody rb, CarStats stats)
        {
            _rb = rb;
            _stats = stats;
            _rb.centerOfMass = centerOfMass.localPosition;
            _rb.mass = _stats.mass;
        }

        private void FixedUpdate()
        {
            AddDownforce();
        }

        private void AddDownforce()
        {
            _rb.AddForce(-transform.up * _stats.downforce * _rb.velocity.magnitude);
        }
    }
}
