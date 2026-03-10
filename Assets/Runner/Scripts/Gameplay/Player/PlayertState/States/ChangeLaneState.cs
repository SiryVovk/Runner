using UnityEngine;

public class ChangeLaneState : PlayerState
{
    public override void EnterState(PlayerStateMachineView view) { }
    public override void ExitState(PlayerStateMachineView view) { }

    public override void UpdateState(PlayerStateMachineView view)
    {
        if (view.MovementSystem.IsCenteredOnLane())
        {
            view.SwitchState(view.RunningState);
        }
    }
}
