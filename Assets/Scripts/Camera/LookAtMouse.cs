using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationVector = transform.up * InputManager.Instance.GetMousePositionDeltaVector().x + -1 * transform.right * InputManager.Instance.GetMousePositionDeltaVector().y;
        rotationVector.z = 0;
        vCam.transform.Rotate(rotationVector);
        Vector3 rot = vCam.transform.rotation.eulerAngles;
        rot.z = 0;
        vCam.transform.rotation = Quaternion.Euler(rot);


    }
}
