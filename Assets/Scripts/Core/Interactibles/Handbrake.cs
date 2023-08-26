using System.Collections;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class Handbrake : MonoBehaviour, IInteractible, IHandbrakeInput
    {
        private Transform parentToRotate;

        private Vector3 startRotation;
        private Vector3 endRotation;

        private Vector3 targetRotation;

        private float smDampTime = 0.2f;

        private SmoothRotation smoothRotation;

        private bool lifted = false;

        [Inject]
        private void Construct(SmoothRotation smoothRotation)
        {
            this.smoothRotation = smoothRotation;
            smoothRotation.SetSmoothTime(smDampTime);
        }

        private void Awake()
        {
            parentToRotate = transform.parent;
            startRotation = parentToRotate.localRotation.eulerAngles;
            endRotation = parentToRotate.localRotation.eulerAngles + new Vector3(-35f, 0, 0);
        }

        public void Interact()
        {
            lifted = !lifted;
            targetRotation = lifted ? endRotation : startRotation;

            StopAllCoroutines();
            StartCoroutine(Rotate());
        }


        private IEnumerator Rotate()
        {
            while (Mathf.Abs(parentToRotate.transform.rotation.eulerAngles.x - targetRotation.x) > 0.2f)
            {
                parentToRotate.transform.localRotation = smoothRotation.RotateAroundXAsix(parentToRotate.transform, targetRotation);
                yield return null;
            }
        }

        public bool GetInput()
        {
            return lifted;
        }
    }
}
