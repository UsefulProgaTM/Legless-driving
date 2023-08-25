using UnityEngine;

[RequireComponent(typeof(CarController))]
public class CarVisuals : MonoBehaviour
{
    #region Wheel Meshes
    [SerializeField]
    private GameObject[] _wheelMeshes;
    [SerializeField]
    private WheelCollider _steerWheel;
    [SerializeField]
    private Transform[] steerableWheels;
    private int _wheelMeshesSize;

    #endregion

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _wheelMeshesSize = _wheelMeshes.Length;
    }

    private void Update()
    {
        SpinWheels();
        SteerWheels();
    }
    private void SpinWheels()
    {
        float localVelocityZ = transform.InverseTransformDirection(rb.velocity).z;
        for (int i = 0; i < _wheelMeshesSize; i++)
        {
            _wheelMeshes[i].transform.localRotation *= Quaternion.Euler(Vector3.right * localVelocityZ);
        }
    }
    private void SteerWheels()
    {
        for (int i = 0; i < steerableWheels.Length; i++)
        {
            steerableWheels[i].localRotation = Quaternion.Euler(0, _steerWheel.steerAngle, 0);
        }
    }
}
