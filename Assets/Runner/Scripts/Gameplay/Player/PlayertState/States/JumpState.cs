using UnityEngine;

public class JumpState : PlayerState
{
    private bool _laneChangedInAir = false;

    private const int LeftLaneChange = -1;
    private const int RightLaneChange = 1;

    public override void EnterState(PlayerStateMachineView playerStateMachineView)
    {
        _laneChangedInAir = false;
        playerStateMachineView.MovementSystem.Jump();
    }

    public override void UpdateState(PlayerStateMachineView playerStateMachineView)
    {
        if (playerStateMachineView.MovementSystem.IsGrounded && playerStateMachineView.MovementSystem.IsFalling())
        {
            playerStateMachineView.SwitchState(playerStateMachineView.RunningState);
        }
    }

    public override void ExitState(PlayerStateMachineView playerStateMachineView) { }

    public override void OnLeft(PlayerStateMachineView playerStateMachineView)
    {
        TryChangeLaneInAir(playerStateMachineView, LeftLaneChange);
    }

    public override void OnRight(PlayerStateMachineView playerStateMachineView)
    {
        TryChangeLaneInAir(playerStateMachineView, RightLaneChange);
    }

    private void TryChangeLaneInAir(PlayerStateMachineView playerStateMachineView, int direction)
    {
        if (_laneChangedInAir)
        {
            return;
        }

        if (playerStateMachineView.LaneSystem.TryMove(direction))
        {
            _laneChangedInAir = true;
        }
    }
}
