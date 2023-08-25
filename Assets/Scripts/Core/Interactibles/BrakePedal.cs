using LeglessDriving;
using UnityEngine;
using LeglessDriving;

public class BrakePedal : BasePedal, IInteractible
{
    public void Interact(bool hasBrick)
    {
        InteractWithBrick(hasBrick);
        InteractWithNoBrick(hasBrick);

    }

    private void Update()
    {
        RotateToTarget();
    }
}
