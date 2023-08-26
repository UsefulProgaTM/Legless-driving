using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform goDownPos;
        [SerializeField]
        private Transform goUpPos;

        private Transform targetPos;

        private Vector3 smDampVelocity;
        private float smDampTime = 0.25f;

        // Start is called before the first frame update
        void Start()
        {
            targetPos = goUpPos;
            InputManager.Instance.OnChangePosition += Instance_OnGoDown;
        }
        private void OnDestroy()
        {
            InputManager.Instance.OnChangePosition -= Instance_OnGoDown;
        }

        private void Instance_OnGoDown()
        {
            targetPos = targetPos == goDownPos ? goUpPos : goDownPos;
        }

        // Update is called once per frame
        void Update()
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos.localPosition, ref smDampVelocity, smDampTime);
        }
    }
}
