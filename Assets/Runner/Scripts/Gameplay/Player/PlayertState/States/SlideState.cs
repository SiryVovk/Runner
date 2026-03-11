
public class SlideState : PlayerState
{
    public override void EnterState(PlayerStateMachineView view)
    {
        view.PlayerColliisionSystem.SetSliding(true);
        view.AnimationSystem.TriggerSlide();
    }

    public override void ExitState(PlayerStateMachineView view)
    {
        view.PlayerColliisionSystem.SetSliding(false);
    }

    public override void UpdateState(PlayerStateMachineView view) { }

    public override void OnAnimationFinished(PlayerStateMachineView view)
    {
        view.SwitchState(view.RunningState);
    }
}
