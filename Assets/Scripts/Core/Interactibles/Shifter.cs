using System.Collections;
using UnityEngine;

namespace LeglessDriving
{
    public class Shifter : MonoBehaviour, IShifter
    {
        [SerializeField]
        private ClutchPedal _clutch;

        [SerializeField]
        private Transform[] shifterPositionsArray;

        [SerializeField]
        private Transform neutralPosition;

        private int currentGearId;

        private Vector3 smDampVelocity;
        private float smDampTime = 0.2f;

        private Vector3 targetGearPos;
        private bool inNeutral = true;

        private enum Gears
        {
            neutral,
            first, 
            second, 
            third, 
            fourth, 
            fifth, 
            reverse,
        }

        private void Awake()
        {
            currentGearId = -1;
        }

        public void ShiftUp()
        {
            if (inNeutral)
            {
                currentGearId++;
                currentGearId = WrapGearID(currentGearId);
            }

            SwitchGear(currentGearId);
        }

        public void ShiftDown()
        {
            if (inNeutral)
            {
                currentGearId--;
                currentGearId = WrapGearID(currentGearId);
            }

            SwitchGear(currentGearId);          
        }

        private void SwitchGear(int id)
        {
            inNeutral = !inNeutral;
            targetGearPos = inNeutral ? neutralPosition.localPosition : shifterPositionsArray[id].localPosition;

            StopAllCoroutines();
            StartCoroutine(MoveToNextGearPos());
        }

        private int WrapGearID(int nextID)
        {
            if (nextID >= shifterPositionsArray.Length)
                return 0;

            if (nextID < 0)
                return shifterPositionsArray.Length - 1;

            return nextID;
        }

        private IEnumerator MoveToNextGearPos()
        {
            while (Mathf.Abs(Vector3.Distance(transform.localPosition, targetGearPos)) > 0.001f)
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetGearPos, ref smDampVelocity, smDampTime);
                yield return null;
            }
        }

        public int GetGearId()
        { 
            return inNeutral ? 0 : currentGearId; 
        }

        public bool IsInNeutral()
        {
            return inNeutral;
        }

        public bool CheckIsClutchEngaged()
        {
            return _clutch.ClutchEnabled;
        }    

        public bool IsReversing()
        {
            return currentGearId == shifterPositionsArray.Length - 1;
        }
    }
}
