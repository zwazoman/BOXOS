using UnityEngine;

public class LightAttackState : AttackState
{
    public override void OnEnter()
    {
        type = ActionType.LightAttack;

        base.OnEnter();

        arm.OnAnimationEnd += stateMachine.Neutral;

        arm.animator.SetTrigger("LightAttack");
        arm.animator.SetFloat("LightAttackSpeed", stats.speed);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnExit() 
    { 
        base.OnExit();

        arm.OnAnimationEnd -= stateMachine.Neutral;
    }
}
