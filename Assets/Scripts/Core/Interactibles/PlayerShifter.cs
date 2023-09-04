using System.Collections;
using UnityEditor.Search;
using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class PlayerShifter : MonoBehaviour, IShifter
    {
        private IClutch _clutch;

        private Transform _transform;

        [Inject]
        private CarSoundManager _soundManager;

        [SerializeField]
        private Transform[] _shifterPositionsArray;

        [SerializeField]
        private Transform _neutralPosition;

        private int currentGearId;
        private int lastGearID;

        private bool recentlyShifted = false;

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
            lastGearID = -1;
            _transform = transform;
            _clutch = clutch;
        }

        public void ShiftGear(int increase)
        {
            lastGearID = currentGearId;
            recentlyShifted = true;

            if (inNeutral)
            {
                if(_clutch.ClutchEnabled)
                {
                    currentGearId += increase;
                    currentGearId = WrapGearID(currentGearId);
                    SwitchGear(currentGearId);
                    return;
                }
                else
                {
                    //don't allow to shift, play sound, maybe shake shifter
                    _soundManager.PlayGearMissedSound();
                    return;
                }
            }
            SwitchGear(currentGearId);
        }

        private void SwitchGear(int id)
        {
            inNeutral = !inNeutral;

            targetGearPos = inNeutral ? _neutralPosition.localPosition : _shifterPositionsArray[id].localPosition;

            if (inNeutral)
                _soundManager.PlayGearShiftNeutralSound();
            else          
                _soundManager.PlayGearShiftSound();

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

        public int GetGearPositionID()
        {
            if (lastGearID < 0)
                return 0;

            return lastGearID;
        }

        //public int GetCurrentGearEngaged()
        //{
        //    int result = 0;
        //    if (recentlyShifted)
        //        if (_clutch.ClutchEnabled)
        //        {
        //            result = lastGearID >= 0 ? lastGearID : 0;
        //            Debug.Log("recentlyShifted + _clutch.ClutchEnabled");
        //        }
        //        else
        //        {
        //            result = currentGearId >= 0 ? currentGearId : 0;
        //            Debug.Log("recentlyShifted + _clutch.ClutchDisabled");
        //        }
        //    else
        //    {
        //        result = currentGearId >= 0 ? currentGearId : 0;
        //        Debug.Log("not recently shifted");
        //    }
        //    return result;
        //}

        public int GetGearAmount()
        {
            return 5;
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
        private void Update()
        {
            if(recentlyShifted)
            {
                if (!_clutch.ClutchEnabled)
                {
                    lastGearID = currentGearId;
                    recentlyShifted = false;
                }
            }
        }
    }
}
