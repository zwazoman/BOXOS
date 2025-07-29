using Unity.Burst.Intrinsics;
using UnityEngine;

public class HeavyAttackState : AttackState
{
    public override void OnEnter()
    {
        type = AttackType.Heavy;

        base.OnEnter();

        arm.OnAnimationEnd += stateMachine.Neutral;

        arm.animator.SetTrigger("HeavyAttack");
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
