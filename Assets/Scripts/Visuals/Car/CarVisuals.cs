using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(CarController))]
public class CarVisuals : MonoBehaviour
{
    #region Wheel Meshes
    [SerializeField]
    private GameObject[] _wheelMeshes;
    private int _wheelMeshesSize;

    #endregion

    private float maxSteerAngle = 35;

    private IMovementInput input;

    private Rigidbody rb;


    private void Awake()
    {
        input = GetComponent<IMovementInput>();
        rb = GetComponent<Rigidbody>();
        _wheelMeshesSize = _wheelMeshes.Length;
    }

    private void Update()
    {
        RotateDriveWheels(input.GetHorizontalInput());
        SpinWheels();
    }

    private void RotateDriveWheels(float angle)
    {
        _wheelMeshes[0].transform.localRotation =
            _wheelMeshes[1].transform.localRotation = 
            Quaternion.Euler(_wheelMeshes[0].transform.localRotation.eulerAngles.x, angle * maxSteerAngle, 0);
    }

    private void SpinWheels()
    {
        float localVelocityZ = transform.InverseTransformDirection(rb.velocity).z;

        for (int i = 0; i < _wheelMeshesSize; i++)
        {
            _wheelMeshes[i].transform.Rotate(Vector3.right * localVelocityZ);
        }
    }
}
