using UnityEngine;

public class ChangeLaneState : PlayerState
{
    public override void EnterState(PlayerStateMachineView playerStateMachinView)
    {

    }

    public override void ExitState(PlayerStateMachineView playerStateMachinView)
    {

    }

    public override void UpdateState(PlayerStateMachineView playerStateMachinView)
    {
        if (playerStateMachinView.MovementSystem.IsCenteredOnLane())
        {
            playerStateMachinView.SwitchState(playerStateMachinView.RunningState);
        }
    }
}
