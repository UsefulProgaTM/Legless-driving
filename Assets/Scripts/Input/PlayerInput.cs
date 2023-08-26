using LeglessDriving;
using System;

public class PlayerInput : IHorizontalInput
{
    public event Action OnHandbrakeInput;

    public float GetHorizontalInput()
    {
        return InputManager.Instance.GetMovementVector().x;
    }
}
