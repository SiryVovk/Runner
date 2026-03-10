using UnityEngine;

public class RunningState : PlayerState
{
    public override void EnterState(PlayerStateMachineView view) { }
    public override void UpdateState(PlayerStateMachineView view) { }
    public override void ExitState(PlayerStateMachineView view) { }

    public override void OnLeft(PlayerStateMachineView view)
    {
        TryChangeLane(view, LeftLaneChange);
    }

    public override void OnRight(PlayerStateMachineView view)
    {
        TryChangeLane(view, RightLaneChange);
    }

    public override void OnJump(PlayerStateMachineView view)
    {
        view.SwitchState(view.JumpState);
    }

    public override void OnSlide(PlayerStateMachineView view)
    {
        view.SwitchState(view.SlideState);
    }

    private void TryChangeLane(PlayerStateMachineView view, int direction)
    {
        if (view.LaneSystem.TryMove(direction))
        {
            view.SwitchState(view.ChangeLaneState);
        }
    }
}
