using System.Collections;
using TMPro;
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

        private SmoothRotation smoothRotation;
        private float smDampTime = 0.2f;

        [Inject]
        private void Construct(Player player, SmoothRotation smoothRotation)
        {
            this.player = player;
            this.smoothRotation = smoothRotation;
            smoothRotation.SetSmoothTime(smDampTime);
        }

        private void Awake()
        {
            targetRotation = new Vector3(parentToRotate.rotation.eulerAngles.x, 0,0);
            freeRotation = new Vector3(parentToRotate.rotation.eulerAngles.x, 0, 0);
            pushedRotation = new Vector3(parentToRotate.rotation.eulerAngles.x, 0, 0);
            pushedRotation += new Vector3(-15, 0, 0);
        }

        public void InteractWithBrick()
        {
            if (brickOnPedal)
            {
                ReactToBrickRemove();
            }
            else if(player.HasBrick)
            {
                ReactToBrickPut();
            }
        }

        public void TryManuallyPushPedal()
        {
            if (!player.HasBrick && !brickOnPedal)
            {
                StartCoroutine(ManuallyPushPedal());
            }
        }

        private IEnumerator ManuallyPushPedal()
        {
            while(InputManager.Instance.IsInteractPressed())
            {
                yield return null;
                targetRotation = pushedRotation;
            }
            targetRotation = freeRotation;
        }

        private void ReactToBrickRemove()
        {
            targetRotation = freeRotation;
            brick.SetActive(false);
            brickOnPedal = false;
            player.PickupBrick();
        }

        private void ReactToBrickPut()
        {
            targetRotation = pushedRotation;
            brick.SetActive(true);
            brickOnPedal = true;
            player.PutBrick();
        }

        private void Update()
        {
            parentToRotate.localRotation = smoothRotation.RotateAroundXAsix(parentToRotate.transform, targetRotation);
        }
    }
}
