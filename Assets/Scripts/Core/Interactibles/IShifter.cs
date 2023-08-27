using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public interface IShifter
    {
        public void Initialize(Transform transform, Transform[] shifterPosArray, Transform neutralPosition, IClutch clutch);
        public void ShiftUp();
        public void ShiftDown();
        public bool IsInNeutral();

        public bool CheckIsClutchEngaged();

        public bool IsReversing();
        public int GetGearID();
    }
}
