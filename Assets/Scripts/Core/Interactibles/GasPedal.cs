using LeglessDriving;

public class GasPedal : BasePedal, IInteractible
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
