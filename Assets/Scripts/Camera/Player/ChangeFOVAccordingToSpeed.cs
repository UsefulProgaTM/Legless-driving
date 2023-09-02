using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class ChangeFOVAccordingToSpeed : MonoBehaviour
    {
        [Inject]
        private CurrentCarStats currentCarStats;

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        private float defaultFoV;

        private float smDampVelocity;
        [SerializeField]
        private float smDamptime = 5f;
        [SerializeField]
        private float multiplier = 10f;

        private Vector3 defaultRotation;
        private float smDampHeadVelocity;
        private float smDampHeadTime = 2f;

        private void Awake()
        {
            defaultFoV = virtualCamera.m_Lens.FieldOfView;
            defaultRotation = Vector3.zero;
        }

        private float lastSpeed = 0;
        public float accelerateAmount;

        // Update is called once per frame
        void Update()
        {
            accelerateAmount = (currentCarStats.speed - lastSpeed) / Time.deltaTime;
            float speedModifier = currentCarStats.speed / 7;

            //transform.Rotate(-accelerateAmount / 250 * Vector3.right);
            //transform.rotation = Quaternion.Euler(new Vector3(Mathf.SmoothDampAngle(transform.rotation.eulerAngles.x, defaultRotation.x, ref smDampHeadVelocity, smDampHeadTime),0,0));

            virtualCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(virtualCamera.m_Lens.FieldOfView, defaultFoV + speedModifier + accelerateAmount * multiplier, ref smDampVelocity, smDamptime);

            lastSpeed = currentCarStats.speed;
        }
    }
}
