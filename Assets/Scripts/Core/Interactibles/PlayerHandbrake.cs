using System.Collections;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class PlayerHandbrake : MonoBehaviour, IHandbrake, IInteractible
    {
        private Transform _parentToRotate;

        [Inject]
        private CarSoundManager _soundManager;

        [Inject]
        private CurrentCarStats _currentCarStats;

        private Vector3 _startRotation;
        private Vector3 _endRotation;

        private Vector3 _targetRotation;

        private const float _smDampTimeAnimation = 0.2f;

        private SmoothRotation _smoothRotation;

        private bool _lifted = false;

        private WheelCollider[] _wheelColliders;
        private CarStats _carStats;

        private float _extremumSlip;
        private float _asymptoteSlip;

        private WheelFrictionCurve _wheelFrictionCurve;

        private float _targetSlip;
        private float _defaultSlip;
        private float _brakeSlip;

        private float _targetStiffness;
        private float _defaultStiffness;
        private float _brakeStiffness;


        private float _smDampVelocity;
        private float _smDampTime = 0.5f;

        [Inject]
        private void Construct(SmoothRotation smoothRotation)
        {
            this._smoothRotation = smoothRotation;
            smoothRotation.SetSmoothTime(_smDampTimeAnimation);
        }

        public void Initialize(CarStats stats, WheelCollider[] wheels)
        {
            _wheelColliders = wheels;
            _carStats = stats;

            _parentToRotate = transform.parent;
            _startRotation = _parentToRotate.localRotation.eulerAngles;
            _endRotation = _parentToRotate.localRotation.eulerAngles + new Vector3(-35f, 0, 0);

            _wheelFrictionCurve = _wheelColliders[0].sidewaysFriction;

            _defaultStiffness = _wheelColliders[0].sidewaysFriction.stiffness;
            _brakeStiffness = 10;

            _defaultSlip = _wheelFrictionCurve.extremumSlip;
            _brakeSlip = 5;
        }

        public void Interact()
        {
            _lifted = !_lifted;
            _currentCarStats.handbrakePulled = _lifted;

            if (_lifted)
                _soundManager.PlayHandbrakePulledSound();
            else           
                _soundManager.PlayHandbrakeReleasedSound();
            

            _targetRotation = _lifted ? _endRotation : _startRotation;

            StopAllCoroutines();
            StartCoroutine(Rotate());
        }

        public void Handbrake()
        {
            if (GetInput())
            {
                _wheelColliders[2].brakeTorque =
                    _wheelColliders[3].brakeTorque = _carStats.brakePower;

                _targetSlip = _brakeSlip;
                _targetStiffness = _brakeStiffness;
            }
            else
            {
                _targetSlip = _defaultSlip;
                _targetStiffness = _defaultStiffness;
            }

            _wheelFrictionCurve.asymptoteSlip = _wheelFrictionCurve.extremumSlip = Mathf.SmoothDamp(_wheelColliders[0].sidewaysFriction.extremumSlip, _targetSlip, ref _smDampVelocity, _smDampTime);
            _wheelFrictionCurve.stiffness = Mathf.SmoothDamp(_wheelColliders[0].sidewaysFriction.stiffness, _targetStiffness, ref _smDampVelocity, _smDampTime);

            _wheelColliders[0].sidewaysFriction =
            _wheelColliders[1].sidewaysFriction =
            _wheelColliders[2].sidewaysFriction =
               _wheelColliders[3].sidewaysFriction = _wheelFrictionCurve;
        }

        private IEnumerator Rotate()
        {
            while (Mathf.Abs(_parentToRotate.transform.rotation.eulerAngles.x - _targetRotation.x) > 0.2f)
            {
                _parentToRotate.transform.localRotation = _smoothRotation.RotateAroundXAsix(_parentToRotate.transform, _targetRotation);
                yield return null;
            }
        }

        public bool GetInput()
        {
            return _lifted;
        }
    }
}
