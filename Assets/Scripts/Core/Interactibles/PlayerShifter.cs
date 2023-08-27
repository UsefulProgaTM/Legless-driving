using System.Collections;
using UnityEngine;

namespace LeglessDriving
{
    public class PlayerShifter : MonoBehaviour, IShifter
    {
        private IClutch _clutch;

        private Transform _transform;

        [SerializeField]
        private Transform[] _shifterPositionsArray;

        [SerializeField]
        private Transform _neutralPosition;

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

        public void Initialize(IClutch clutch)
        {
            currentGearId = -1;
            _transform = transform;
            _clutch = clutch;
        }

        public void ShiftUp()
        {
            if (inNeutral)
            {
                if(_clutch.ClutchEnabled)
                {
                    currentGearId++;
                    currentGearId = WrapGearID(currentGearId);
                    SwitchGear(currentGearId);
                    return;
                }
                else
                {
                    //don't allow to shift, play sound, maybe shake shifter
                    return;
                }
            }
            SwitchGear(currentGearId);
        }

        public void ShiftDown()
        {
            if (inNeutral)
            {
                if (_clutch.ClutchEnabled)
                {
                    currentGearId--;
                    currentGearId = WrapGearID(currentGearId);
                    SwitchGear(currentGearId);
                    //don't allow to shift, play sound, maybe shake shifter
                    return;
                }
                else
                {
                    return;
                }
            }
            SwitchGear(currentGearId);
        }

        private void SwitchGear(int id)
        {
            inNeutral = !inNeutral;
            targetGearPos = inNeutral ? _neutralPosition.localPosition : _shifterPositionsArray[id].localPosition;

            StopAllCoroutines();
            StartCoroutine(MoveToNextGearPos());
        }

        private int WrapGearID(int nextID)
        {
            if (nextID >= _shifterPositionsArray.Length)
                return 0;

            if (nextID < 0)
                return _shifterPositionsArray.Length - 1;

            return nextID;
        }

        private IEnumerator MoveToNextGearPos()
        {
            while (Mathf.Abs(Vector3.Distance(_transform.localPosition, targetGearPos)) > 0.001f)
            {
                _transform.localPosition = Vector3.SmoothDamp(_transform.localPosition, targetGearPos, ref smDampVelocity, smDampTime);
                yield return null;
            }
        }

        public int GetGearID()
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
            return currentGearId == _shifterPositionsArray.Length - 1;
        }
    }
}
