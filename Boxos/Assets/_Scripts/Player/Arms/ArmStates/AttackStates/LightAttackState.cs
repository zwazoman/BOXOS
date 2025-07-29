using UnityEngine;

public class LightAttackState : AttackState
{
    public override void OnEnter()
    {
        type = AttackType.Light;

        base.OnEnter();

        arm.OnAnimationEnd += stateMachine.Neutral;

        arm.animator.SetTrigger("LightAttack");
        arm.player.UpdateHeat(stats.Value.heatCost);
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
