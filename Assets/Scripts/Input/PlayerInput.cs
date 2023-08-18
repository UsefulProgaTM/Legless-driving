using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour, IMovementInput
{
    Vector2 input = new Vector2();
    public event Action OnHandbrakeInput;

    // Update is called once per frame
    void Update()
    {
        input = InputManager.Instance.GetMovementVector();
        InputManager.Instance.OnHandbrakePerformed += Instance_OnHandbrakePerformed;
    }
    private void OnDestroy()
    {
        InputManager.Instance.OnHandbrakePerformed -= Instance_OnHandbrakePerformed;
    }
    private void Instance_OnHandbrakePerformed()
    {
        OnHandbrakeInput?.Invoke(); 
    }

    public float GetHorizontalInput()
    {
        return input.x;
    }
    public float GetForwardInput()
    {
        return input.y;
    }
}
