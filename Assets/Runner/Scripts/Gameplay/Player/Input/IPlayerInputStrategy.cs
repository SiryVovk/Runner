using System;

public interface IPlayerInputStrategy
{
    public event Action LeftPerformed;
    public event Action RightPerformed;
    public event Action SlidePerformed;
    public event Action JumpPerformed;

    public void Enable();
    public void Disable();
}
