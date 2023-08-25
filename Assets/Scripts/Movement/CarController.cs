using LeglessDriving;
using UnityEngine;
using Zenject;

public class CarController : MonoBehaviour
{
    private IMovementInput input;

    [SerializeField]
    private CurrentCarStats currentCarStats;
    [SerializeField]
    private CarStats carStats;

    private Rigidbody rb;

    [SerializeField]
    private GameObject centerOfMass;
    private float downforceAmount = 50;

    #region Wheel Colliders
    [SerializeField]
    private WheelCollider[] _wheelColliders;
    #endregion
    private float maxSteerAngle = 35;

    [Inject]
    public void Construct(IMovementInput input)
    {
        this.input = input;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
        rb.mass = carStats.mass;
    }

    private void FixedUpdate()
    {
        currentCarStats.speed = transform.InverseTransformDirection(rb.velocity).z;
        SteerDriveWheels(input.GetHorizontalInput());
        Accelerate(input.GetForwardInput());
        Break(input.GetForwardInput());
        AddDownforce();
    }

    private void SteerDriveWheels(float input)
    {
        _wheelColliders[0].steerAngle = input * carStats.steerAngle;
        _wheelColliders[1].steerAngle = input * carStats.steerAngle;
    }

    private void Accelerate(float input)
    {
        _wheelColliders[0].motorTorque =
        _wheelColliders[1].motorTorque =
        _wheelColliders[2].motorTorque =
        _wheelColliders[3].motorTorque = input * carStats.HP;
    }

    private void Break(float input)
    {
        float localVelocityZ = transform.InverseTransformDirection(rb.velocity).z;
        if (localVelocityZ > 0)
        {
            if (input < 0)
            {
                _wheelColliders[0].brakeTorque =
                _wheelColliders[1].brakeTorque =
                _wheelColliders[2].brakeTorque =
                _wheelColliders[3].brakeTorque = -input * carStats.brakePower;
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
                _wheelColliders[0].brakeTorque =
                _wheelColliders[1].brakeTorque =
                _wheelColliders[2].brakeTorque =
                _wheelColliders[3].brakeTorque = carStats.brakePower;
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


    private void Handbrake()
    {
        _wheelColliders[2].brakeTorque =
        _wheelColliders[3].brakeTorque = carStats.brakePower * 5;
    }

    private void AddDownforce()
    {
        rb.AddForce(-transform.up * downforceAmount * rb.velocity.magnitude);
    }
}
