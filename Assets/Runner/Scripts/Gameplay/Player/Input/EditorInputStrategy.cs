
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static RunnerInputAction;

public class EditorInputStrategy : IPlayerInputStrategy, IEditorActions
{
    public event Action LeftPerformed;
    public event Action RightPerformed;
    public event Action SlidePerformed;
    public event Action JumpPerjormed;

    private RunnerInputAction _inputAction;

    public EditorInputStrategy()
    {
        _inputAction = new RunnerInputAction();
        _inputAction.Editor.SetCallbacks(this);
    }
    public void Enable()
    {
        _inputAction.Enable();
    }

    public void Disable()
    {
        _inputAction.Disable();
    }


    public void OnLeft(InputAction.CallbackContext context)
    {
        LeftPerformed?.Invoke();
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        RightPerformed?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpPerjormed?.Invoke();
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        SlidePerformed?.Invoke();
    }
}
