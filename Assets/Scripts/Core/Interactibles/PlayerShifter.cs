using System.Collections;
using UnityEngine;

namespace LeglessDriving
{
    public class PlayerShifter : IShifter
    {
        private IClutch _clutch;

        private Transform _transform;
        private MonoBehaviour _mono;

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

        public void Initialize(Transform transform, Transform[] shifterPosArray, Transform neutralPosition, IClutch clutch)
        {
            currentGearId = -1;
            _transform = transform;
            _mono = _transform.gameObject.GetComponent<MonoBehaviour>();
            _shifterPositionsArray = shifterPosArray;
            _neutralPosition = neutralPosition;
            _clutch = clutch;
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
            targetGearPos = inNeutral ? _neutralPosition.localPosition : _shifterPositionsArray[id].localPosition;

            _mono.StopAllCoroutines();
            _mono.StartCoroutine(MoveToNextGearPos());
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
