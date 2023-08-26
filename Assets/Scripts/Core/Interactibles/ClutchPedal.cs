using LeglessDriving;

public class ClutchPedal : BasePedal, IInteractible, IClutch
{
    public void Interact()
    {
        InteractWithBrick();
        TryManuallyPushPedal();
    }

    public bool ClutchEnabled
    {
        get {return targetRotation == pushedRotation;}
    }
}
