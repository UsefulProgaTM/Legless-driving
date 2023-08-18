using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class CarController : MonoBehaviour
{
    private IMovementInput input;
   
    private Rigidbody rb;

    //use SO
    [SerializeField]
    private float horsePower = 1500;
    [SerializeField]
    private float brakePower = 10000;

    [SerializeField]
    private GameObject centerOfMass;
    private float downforceAmount = 50;

    #region Wheel Colliders
    [SerializeField]
    private WheelCollider _frontLeftWheelCollider;
    [SerializeField]
    private WheelCollider _frontRightWheelCollider;

    [SerializeField]
    private WheelCollider _rearLeftWheelCollider;
    [SerializeField]
    private WheelCollider _rearRightWheelCollider;
    #endregion
    private float maxSteerAngle = 35;

    private Vector3 _lastPosition;

    private void Awake()
    {
        input = GetComponent<IMovementInput>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    void Start()
    {
        input.OnHandbrakeInput += Input_OnHandbrakeInput;
       
    }

    private void OnDestroy()
    {
        input.OnHandbrakeInput -= Input_OnHandbrakeInput;
    }

    private void Input_OnHandbrakeInput()
    {
        Handbrake();
    }

    private void FixedUpdate()
    {
        SteerDriveWheels(input.GetHorizontalInput());
        Accelerate(input.GetForwardInput());
        Break(input.GetForwardInput());
        AddDownforce();
    }

    private void SteerDriveWheels(float input)
    {
        _frontLeftWheelCollider.steerAngle = input * maxSteerAngle;
        _frontRightWheelCollider.steerAngle = input * maxSteerAngle;
    }

    private void Accelerate(float input)
    {
        _frontLeftWheelCollider.motorTorque = _frontRightWheelCollider.motorTorque = input * horsePower;
    }

    private void Break(float input)
    {
        float localVelocityZ = transform.InverseTransformDirection(rb.velocity).z;
        if (localVelocityZ > 0)
        {
            if (input < 0)
            {
                _frontLeftWheelCollider.brakeTorque =
                _frontRightWheelCollider.brakeTorque =
                _rearRightWheelCollider.brakeTorque =
                _rearLeftWheelCollider.brakeTorque = -input * brakePower;
            }
            else
            {
                RemoveBreakForce();
            }
        }
        else
        {
            if (input > 0)
            {
                _frontLeftWheelCollider.brakeTorque =
                _frontRightWheelCollider.brakeTorque =
                _rearRightWheelCollider.brakeTorque =
                _rearLeftWheelCollider.brakeTorque =  brakePower;
            }
            else
            {
                RemoveBreakForce();
            }
        }      
    }

    private void RemoveBreakForce()
    {
        _frontLeftWheelCollider.brakeTorque =
                _frontRightWheelCollider.brakeTorque =
                _rearRightWheelCollider.brakeTorque =
                _rearLeftWheelCollider.brakeTorque = 0;
    }

    private void QuickRecoveryAfterReverse(float input)
    {
        if (input > 0 && rb.velocity.z < 0)
        {
            _frontLeftWheelCollider.brakeTorque =
        _frontRightWheelCollider.brakeTorque =
        _rearRightWheelCollider.brakeTorque =
        _rearLeftWheelCollider.brakeTorque = brakePower;
        }
        else
        {
            _frontLeftWheelCollider.brakeTorque =
        _frontRightWheelCollider.brakeTorque =
        _rearRightWheelCollider.brakeTorque =
        _rearLeftWheelCollider.brakeTorque = 0;
        }
    }

    private void Handbrake()
    {
        _rearRightWheelCollider.brakeTorque =
        _rearLeftWheelCollider.brakeTorque = brakePower * 2;
    }

    private void AddDownforce()
    {
        rb.AddForce(-transform.up * downforceAmount * rb.velocity.magnitude);
    }
}
