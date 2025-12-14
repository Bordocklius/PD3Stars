using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "Scriptable Objects/InputReaderSO")]
public class InputReaderSO : ScriptableObject
{
    // InputAction generated script
    private InputSystem_Actions _inputActions;

    private InputAction _moveAction;
    private InputAction _paAction;

    public event EventHandler<InputReaderEventArgs> Move;
    public event EventHandler<InputReaderEventArgs> PA;

    private void OnEnable()
    {
        if(_inputActions == null)
            _inputActions = new InputSystem_Actions();

        _inputActions.Enable();
        _moveAction = _inputActions.PlayerInput.Move;
        _paAction = _inputActions.PlayerInput.PrimaryAttack;

        _moveAction.Enable();
        _moveAction.performed += Move_Performed;
        _moveAction.canceled += Move_Performed;

        _paAction.Enable();
        _paAction.performed += PA_Performed;

    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.Disable();
        _moveAction.performed -= Move_Performed;
        _moveAction.canceled -= Move_Performed;

        _paAction.Disable();
        _paAction.performed -= PA_Performed;
    }

    private void Move_Performed(InputAction.CallbackContext ctx)
    {
        Move?.Invoke(this, new InputReaderEventArgs(ctx));            
    }

    protected virtual void PA_Performed(InputAction.CallbackContext ctx)
    {
        PA?.Invoke(this, new InputReaderEventArgs(ctx));
    }
}


public class InputReaderEventArgs : EventArgs
{
    public InputAction.CallbackContext Ctx { get; private set; }

    public InputReaderEventArgs(InputAction.CallbackContext newValue)
    {
        Ctx = newValue;
    }
}

