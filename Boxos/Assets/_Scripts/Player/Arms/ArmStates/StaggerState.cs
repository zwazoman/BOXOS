using UnityEngine;

public class StaggerState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Stagger");

        arm.player.OnKick += stateMachine.Neutral;
    }

    public override void OnExit()
    {
        arm.player.OnKick -= stateMachine.Neutral;
    }
}
