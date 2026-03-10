using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static RunnerInputAction;

public class BuildInputStrategy : IPlayerInputStrategy, IPhoneActions
{
    public event Action LeftPerformed;
    public event Action RightPerformed;
    public event Action SlidePerformed;
    public event Action JumpPerformed;

    private RunnerInputAction _inputAction;

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private const float SwipeThreshold = 25f;

    public BuildInputStrategy()
    {
        _inputAction = new RunnerInputAction();

        _inputAction.Phone.SetCallbacks(this);
    }
    public void Enable()
    {
        _inputAction.Enable();
    }

    public void Disable()
    {
       _inputAction.Disable();
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _startPosition = _inputAction.Phone.TouchPosition.ReadValue<Vector2>();
        }

        if(context.canceled)
        {
            _endPosition = _inputAction.Phone.TouchPosition.ReadValue<Vector2>();
            Swipe();
        }
    }

    private void Swipe()
    {
        Vector2 deltaChange = _endPosition - _startPosition;

        if (Mathf.Abs(deltaChange.x) < SwipeThreshold && Mathf.Abs(deltaChange.y) < SwipeThreshold)
        {
            return;
        }

        if (Math.Abs(deltaChange.x) > Math.Abs(deltaChange.y))
        {
            if (deltaChange.x > 0)
            {
                RightPerformed?.Invoke();
            }
            else
            {
                LeftPerformed?.Invoke();
            }
        }
        else
        {
            if (deltaChange.y > 0)
            {
                JumpPerformed?.Invoke();
            }
            else
            {
                SlidePerformed?.Invoke();
            }
        }
    }

    public void OnTouchPosition(InputAction.CallbackContext context)
    {
        return;
    }
}
