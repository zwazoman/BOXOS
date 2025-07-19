using UnityEngine;

public class StaggerState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Stagger");

        arm.OnAnimationCycle += stateMachine.Neutral;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= stateMachine.Neutral;
    }
}
