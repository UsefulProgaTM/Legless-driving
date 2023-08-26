using LeglessDriving;

public class GasPedal : BasePedal, IInteractible, IGasInput
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
