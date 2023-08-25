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
            targetPos = transform;
            InputManager.Instance.OnGoUp += Instance_OnGoUp;
            InputManager.Instance.OnGoDown += Instance_OnGoDown;
        }
        private void OnDestroy()
        {
            InputManager.Instance.OnGoUp -= Instance_OnGoUp;
            InputManager.Instance.OnGoDown -= Instance_OnGoDown;
        }

        private void Instance_OnGoDown()
        {
            targetPos = goDownPos;
        }

        private void Instance_OnGoUp()
        {
            targetPos = goUpPos;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos.position, ref smDampVelocity, smDampTime);
        }
    }
}
