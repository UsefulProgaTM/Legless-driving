using LeglessDriving;

public class BrakePedal : BasePedal, IInteractible, IBreakInput
{
    public void Interact()
    {
        InteractWithBrick();
        TryManuallyPushPedal();
    }

    public float GetInput()
    {
        return targetRotation == pushedRotation ? 1 : 0;
    }
}
