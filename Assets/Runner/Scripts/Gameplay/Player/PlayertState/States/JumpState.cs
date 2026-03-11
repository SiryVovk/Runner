
public class JumpState : PlayerState
{
    private bool _laneChangedInAir = false;

    public override void EnterState(PlayerStateMachineView view)
    {
        _laneChangedInAir = false;
        view.PlayerColliisionSystem.SetJumping(true);
        view.AnimationSystem.TriggerJump();
    }

    public override void ExitState(PlayerStateMachineView view)
    {
        view.PlayerColliisionSystem.SetJumping(false);
    }

    public override void UpdateState(PlayerStateMachineView view) { }

    public override void OnLeft(PlayerStateMachineView view)
    {
        TryChangeLaneInAir(view, LeftLaneChange);
    }

    public override void OnRight(PlayerStateMachineView view)
    {
        TryChangeLaneInAir(view, RightLaneChange);
    }

    public override void OnAnimationFinished(PlayerStateMachineView view)
    {
        view.SwitchState(view.RunningState);
    }

    private void TryChangeLaneInAir(PlayerStateMachineView view, int direction)
    {
        if (_laneChangedInAir) return;

        if (view.LaneSystem.TryMove(direction))
        {
            _laneChangedInAir = true;
        }
    }
}
