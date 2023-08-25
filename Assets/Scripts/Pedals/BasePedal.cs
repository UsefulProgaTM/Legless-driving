using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class BasePedal : MonoBehaviour
    {
        [SerializeField]
        protected GameObject brick;

        [Inject]
        protected Player player;

        [SerializeField]
        private Transform parentToRotate;

        protected Vector3 targetRotation;
        protected Vector3 pushedRotation;
        protected Vector3 freeRotation;

        protected bool brickOnPedal = false;

        private void Awake()
        {
            targetRotation = new Vector3(parentToRotate.rotation.eulerAngles.x, 0,0);
            freeRotation = new Vector3(parentToRotate.rotation.eulerAngles.x, 0, 0);
            pushedRotation = new Vector3(parentToRotate.rotation.eulerAngles.x, 0, 0);
            pushedRotation += new Vector3(-15, 0, 0);
        }

        private float velocity;
        private float smDampTime = 0.2f;

        public void InteractWithBrick(bool playerHasBrick)
        {
            if(playerHasBrick || brickOnPedal)
            {
                brick.SetActive(playerHasBrick);
                brickOnPedal = brick.activeSelf;
                if (brickOnPedal)
                {
                    ReactToBrickPut();
                }
                else
                {
                    ReactToBrickRemove();
                }
            }
        }

        public void InteractWithNoBrick(bool playerHasBrick)
        {
            if(!playerHasBrick && !brickOnPedal)
            {
                Debug.Log("As intended");
            }
        }

        private void ReactToBrickRemove()
        {
            targetRotation = freeRotation;
            player.PickupBrick();
        }

        private void ReactToBrickPut()
        {
            targetRotation = pushedRotation;
            player.PutBrick();
        }

        protected void RotateToTarget()
        {
            parentToRotate.transform.rotation =
                Quaternion.Euler(
                    new Vector3(
                        Mathf.SmoothDampAngle(parentToRotate.transform.rotation.eulerAngles.x,
                        targetRotation.x,
                        ref velocity,
                    smDampTime), 0, 0));
        }
    }
}
