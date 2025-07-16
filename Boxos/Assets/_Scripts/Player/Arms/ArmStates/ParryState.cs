using UnityEngine;

public class ParryState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Parry");
        arm.OnAnimationCycle += stateMachine.Neutral;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= stateMachine.Neutral;
    }
}
