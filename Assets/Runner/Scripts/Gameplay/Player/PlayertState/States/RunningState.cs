using UnityEngine;

public class RunningState : PlayerState
{
    private const int LeftLaneChange = -1;
    private const int RightLaneChange = 1;

    public override void EnterState(PlayerStateMachineView playerStateMachineView) { }

    public override void UpdateState(PlayerStateMachineView playerStateMachineView) { }

    public override void ExitState(PlayerStateMachineView playerStateMachineView) { }

    public override void OnLeft(PlayerStateMachineView playerStateMachineView)
    {
        TryChangeLane(playerStateMachineView, LeftLaneChange);
    }

    public override void OnRight(PlayerStateMachineView playerStateMachineView)
    {
        TryChangeLane(playerStateMachineView, RightLaneChange);
    }

    public override void OnJump(PlayerStateMachineView playerStateMachineView)
    {
        if (!playerStateMachineView.MovementSystem.IsGrounded)
        {
            return;
        }

        playerStateMachineView.SwitchState(playerStateMachineView.JumpState);
    }

    public override void OnSlide(PlayerStateMachineView playerStateMachineView)
    {
        if(!playerStateMachineView.MovementSystem.IsGrounded)
        {
            return;
        }

        playerStateMachineView.SwitchState(playerStateMachineView.SlideState);
    }

    private void TryChangeLane(PlayerStateMachineView playerStateMachineView, int direction)
    {
        if (playerStateMachineView.LaneSystem.TryMove(direction))
        {
            playerStateMachineView.SwitchState(playerStateMachineView.ChangeLaneState);
        }
    }
}
