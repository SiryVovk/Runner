
public abstract class PlayerState
{
    protected const int LeftLaneChange = -1;
    protected const int RightLaneChange = 1;

    public abstract void EnterState(PlayerStateMachineView view);
    public abstract void UpdateState(PlayerStateMachineView view);
    public abstract void ExitState(PlayerStateMachineView view);

    public virtual void OnLeft(PlayerStateMachineView view) { }
    public virtual void OnRight(PlayerStateMachineView view) { }
    public virtual void OnJump(PlayerStateMachineView view) { }
    public virtual void OnSlide(PlayerStateMachineView view) { }
    public virtual void OnAnimationFinished(PlayerStateMachineView view) { }
}
