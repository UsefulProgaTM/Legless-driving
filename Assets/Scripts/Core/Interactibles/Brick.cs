using System.Collections;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class Brick : MonoBehaviour, IInteractible
    {
        private Player player;
        private SmoothRotation smoothRotation;

        [SerializeField]
        private BasePedal pedal;

        private GameObject parentToRotate;

        private Vector3 startRotation;
        private Vector3 endRotation;

        private float smDampVelocity;
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
            parentToRotate = transform.parent.gameObject;
            startRotation = parentToRotate.transform.localRotation.eulerAngles;
            endRotation = parentToRotate.transform.localRotation.eulerAngles + new Vector3(15,0,0);
        }

        public void Interact()
        {
            player.PickupBrick();

            if (pedal != null)
            {
                pedal.InteractWithBrick();
            }

            gameObject.transform.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if(pedal != null)
            {
                StopAllCoroutines();
                StartCoroutine(Rotate());
            }
        }

        private IEnumerator Rotate()
        {
            parentToRotate.transform.localRotation = Quaternion.Euler(startRotation);
            while (parentToRotate.transform.localRotation.eulerAngles.x < endRotation.x - 0.2f)
            {
                parentToRotate.transform.localRotation = smoothRotation.RotateAroundXAsix(parentToRotate.transform, endRotation);
                yield return null;
            }
        }
    }
}
