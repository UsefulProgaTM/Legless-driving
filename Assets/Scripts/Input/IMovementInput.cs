using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementInput
{
    public float GetHorizontalInput();
    public float GetForwardInput();
    public event Action OnHandbrakeInput;
}
