using LeglessDriving;
using UnityEngine;
using LeglessDriving;

public class ClutchPedal : BasePedal, IInteractible
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
