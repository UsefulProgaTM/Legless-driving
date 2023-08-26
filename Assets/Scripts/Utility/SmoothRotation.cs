
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace LeglessDriving
{
    public class SmoothRotation
    {
        private float smDampTime;
        private float smDampVelocity;

        public SmoothRotation()
        {
            smDampTime = 0.2f;
        }

        public SmoothRotation(float smDampTime)
        {
            this.smDampTime = smDampTime;
        }

        public void SetSmoothTime(float time)
        {
            smDampTime = time;
        }

        public Quaternion RotateAroundXAsix(Transform target, Vector3 targetRotation)
        {
            return Quaternion.Euler(
                    new Vector3(
                        Mathf.SmoothDampAngle(target.transform.localRotation.eulerAngles.x,
                        targetRotation.x,
                        ref smDampVelocity,
                    smDampTime), 0, 0));
        }
    }
}
