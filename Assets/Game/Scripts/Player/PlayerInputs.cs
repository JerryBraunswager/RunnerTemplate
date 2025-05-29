using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputs : MonoBehaviour
{
    private Vector3 _mouseScreen;

    public event Action<float, CallbackContext> Clicked;

    public void OnClick(CallbackContext context)
    {
        Clicked?.Invoke(_mouseScreen.x, context);
    }

    public void WriteMousePosition(CallbackContext context)
    {
        _mouseScreen = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
}
