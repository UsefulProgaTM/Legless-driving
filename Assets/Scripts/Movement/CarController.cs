using LeglessDriving;
using UnityEngine;
using Zenject;

public class CarController : MonoBehaviour
{
    //[SerializeField]
    //private BrakePedal breakInput;
    //private IHorizontalInput horizontalInput;
    //[SerializeField]
    //private Handbrake handbrakeInput;

    //[SerializeField]
    //private Shifter shifter;

    [SerializeField]
    private CurrentCarStats _currentCarStats;

    [SerializeField]
    private CarStats _carStats;

    [SerializeField]    
    private Shifter _shifter;

    private Body _body;
    private Engine _engine;
    private Breaks _breaks;
    private Handling _handling;
    private Transmission _transmission;
    //[SerializeField]
    //private Transmission _transmission;

    private Rigidbody _rb;

    #region Wheel Colliders
    [SerializeField]
    private WheelCollider[] _wheelColliders;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _body = GetComponent<Body>();
        _engine = GetComponent<Engine>();
        _breaks = GetComponent<Breaks>();
        _handling = GetComponent<Handling>();
        _transmission = GetComponent<Transmission>();

        _body.Initialize(_rb, _carStats);
        _transmission.Initialize(_carStats, _wheelColliders[0], _shifter);
        _engine.Initialize(_wheelColliders, _carStats, _transmission, _shifter);
        _breaks.Initialize(_rb, _carStats, _wheelColliders);
        _handling.Initialize(_wheelColliders, _carStats);
    }

    private void FixedUpdate()
    {
        _currentCarStats.speed = transform.InverseTransformDirection(_rb.velocity).z;
    }
}
