using LeglessDriving;
using UnityEngine;
using Zenject;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private CurrentCarStats _currentCarStats;
    
    private EngineSoundManager _engineSoundManager;

    [SerializeField]
    private CarStats _carStats;

    [SerializeField]
    private Transform _centerOfMass;

    private IBody _body;
    private IEngine _engine;
    private IBrakes _breaks;
    private IHandbrake _handbrake;
    private IHandling _handling;
    private ITransmission _transmission;
    private IShifter _shifter;
    private IClutch _clutch;

    private IGasInput _gasInput;
    private IHorizontalInput _horizontalInput;
    private IBreakInput _breakInput;

    private Rigidbody _rb;

    #region Wheel Colliders
    [SerializeField]
    private WheelCollider[] _wheelColliders;
    #endregion

    [Inject]
    private void Construct(IBody body, IEngine engine, IBrakes breaks, IHandbrake handbrake,
        IHandling handling, ITransmission transmission, IShifter shifter,
        IClutch clutch, IGasInput gasInput,
        IHorizontalInput horizontalInput, IBreakInput breakInput)
    {
        _body = body;
        _engine = engine;
        _breaks = breaks;
        _handbrake = handbrake;
        _handling = handling;
        _transmission = transmission;
        _shifter = shifter;
        _clutch = clutch;
        _gasInput = gasInput;
        _horizontalInput = horizontalInput;
        _breakInput = breakInput;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _engineSoundManager = GetComponent<EngineSoundManager>();   

        _body.Initialize(_rb, _carStats, transform, _centerOfMass);
        _engine.Initialize(_wheelColliders, _carStats, _shifter);
        _breaks.Initialize(_carStats, _wheelColliders);
        _handbrake.Initialize(_carStats, _wheelColliders);
        _handling.Initialize(_wheelColliders, _carStats);
        _transmission.Initialize(_carStats, _wheelColliders[0], _shifter);
        _shifter.Initialize(_clutch);
        _engineSoundManager.Initialize(_transmission, _carStats);
    }

    private void Update()
    {
        _currentCarStats.speed = _wheelColliders[0].rpm * _wheelColliders[0].radius  / 60;//transform.InverseTransformDirection(_rb.velocity).z;
    }

    private void FixedUpdate()
    {
        _body.AddDownforce();
        _engine.Accelerate(_gasInput.GetInput(), _transmission.EvaluateRPM());
        _breaks.Break(_breakInput.GetInput());
        _handling.SteerDriveWheels(_horizontalInput.GetHorizontalInput());
        _handbrake.Handbrake();
    }
}
