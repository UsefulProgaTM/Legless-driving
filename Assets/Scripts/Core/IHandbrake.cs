using UnityEngine;

namespace LeglessDriving
{
    public interface IHandbrake
    {
        public void Initialize(CarStats stats, WheelCollider[] wheels);
        public void Handbrake(bool enabled);
    }
}
