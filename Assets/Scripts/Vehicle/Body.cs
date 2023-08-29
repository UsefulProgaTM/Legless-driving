using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class Body : IBody
    {
        private Rigidbody _rb;
        private Transform transform;
        private CarStats _carStats;

        public void Initialize(Rigidbody rb, CarStats stats, Transform transform, Transform centerOfMass)
        {
            _rb = rb;
            _rb.centerOfMass = centerOfMass.localPosition;
            _rb.mass = stats.mass;
            _carStats = stats;
            this.transform = transform;
        }

        public void AddDownforce()
        {
            _rb.AddForce(_carStats.downforce * _rb.velocity.magnitude * -transform.up);
        }
    }
}
