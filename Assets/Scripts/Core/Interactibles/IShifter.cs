using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public interface IShifter
    {
        public void Initialize(IClutch clutch);
        public void ShiftGear(int i);
        public bool IsInNeutral();

        public bool CheckIsClutchEngaged();

        public bool IsReversing();
        public int GetGearPositionID();
        public int GetGearAmount();
    }
}
