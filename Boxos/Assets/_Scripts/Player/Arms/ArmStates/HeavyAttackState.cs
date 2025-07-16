using UnityEngine;

public class HeavyAttackState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("HeavyAttack");
        arm.OnAnimationEnd += stateMachine.Neutral;
    }

    public override void OnExit()
    {
        arm.OnAnimationEnd -= stateMachine.Neutral;
    }
}
