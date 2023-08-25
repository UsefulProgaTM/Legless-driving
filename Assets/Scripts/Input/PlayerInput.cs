using System;

public class PlayerInput : IMovementInput
{
    public event Action OnHandbrakeInput;

    public float GetHorizontalInput()
    {
        return InputManager.Instance.GetMovementVector().x;
    }
    public float GetForwardInput()
    {
        return InputManager.Instance.GetMovementVector().y;
    }

}
