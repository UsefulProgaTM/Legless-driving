using LeglessDriving;
using UnityEngine;
using Zenject;

public class CarInteriorVisuals : MonoBehaviour
{
    [SerializeField]
    private Transform wheel;

    private float targetAngle;
    private float smDampVelocity;
    private float smDampTime = 0.2f;


    private float maxSteerAngle = 35;

    [SerializeField]
    private Transform shifter;

    private IHorizontalInput input;

    [Inject]
    public void Construct(IHorizontalInput input)
    {
        this.input = input;
    }

    private void Update()
    {
        wheel.transform.localRotation = Quaternion.Euler(0, 0, SmoothWheelRotation());
    }

    private float SmoothWheelRotation()
    {
        targetAngle = -input.GetHorizontalInput() * maxSteerAngle;
        float result = Mathf.SmoothDampAngle(wheel.localRotation.eulerAngles.z, targetAngle, ref smDampVelocity, smDampTime);
        return result;
    }
}
