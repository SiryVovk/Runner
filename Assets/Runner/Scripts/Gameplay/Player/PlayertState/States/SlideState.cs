using UnityEngine;

public class SlideState : PlayerState
{
    public override void EnterState(PlayerStateMachineView playerStateMachinView)
    {
        playerStateMachinView.MovementSystem.Slide();
    }

    public override void ExitState(PlayerStateMachineView playerStateMachinView)
    {

    }

    public override void UpdateState(PlayerStateMachineView playerStateMachinView)
    {
        if(!playerStateMachinView.MovementSystem.IsSliding)
        {
            playerStateMachinView.SwitchState(playerStateMachinView.RunningState);
        }
    }
}
