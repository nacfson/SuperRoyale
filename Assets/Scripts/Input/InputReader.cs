using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, InputControls.IMAKActions
{
    private InputControls _inputControls;

    public delegate void InputEventListener();
    public delegate void InputEventListener<in T>(T value);


    public event InputEventListener<Vector2> OnMovementEvent = null;
    public event InputEventListener<Vector2> OnMouseMoveEvent = null;
    public event InputEventListener OnShootEvent = null;
    public event InputEventListener<bool> OnRClickEvent = null;

    private void OnEnable()
    {
        if (_inputControls == null)
        {
            _inputControls = new InputControls();
            _inputControls.MAK.SetCallbacks(this);
        }
        _inputControls.MAK.Enable();
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector2 mousePos = context.ReadValue<Vector2>();
        OnMouseMoveEvent?.Invoke(mousePos);
    }

    void InputControls.IMAKActions.OnMovement(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        OnMovementEvent?.Invoke(movement);
    }

    void InputControls.IMAKActions.OnRoll(InputAction.CallbackContext context)
    {
    }

    void InputControls.IMAKActions.OnLClick(InputAction.CallbackContext context)
    {
    }

    void InputControls.IMAKActions.OnRClick(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnRClickEvent?.Invoke(true);
        }
        else if(context.canceled)
        {
            OnRClickEvent?.Invoke(false);
        }
    }

    void InputControls.IMAKActions.OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnShootEvent?.Invoke();
        }
    }

    void InputControls.IMAKActions.OnAim(InputAction.CallbackContext context)
    {

    }


}
