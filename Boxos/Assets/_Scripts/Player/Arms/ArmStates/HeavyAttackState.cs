using UnityEngine;

public class HeavyAttackState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("HeavyAttack");
        arm.OnAnimationCycle += stateMachine.Neutral;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= stateMachine.Neutral;
    }
}
