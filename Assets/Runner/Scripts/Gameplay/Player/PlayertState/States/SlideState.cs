using UnityEngine;

public class SlideState : PlayerState
{
    public override void EnterState(PlayerStateMachineView playerStateMachinView)
    {
    }

    public override void ExitState(PlayerStateMachineView playerStateMachinView)
    {

    }

    public override void UpdateState(PlayerStateMachineView playerStateMachinView)
    {
        if(!playerStateMachinView.PlayerColliisionSystem.IsSliding)
        {
            playerStateMachinView.SwitchState(playerStateMachinView.RunningState);
        }
    }
}
