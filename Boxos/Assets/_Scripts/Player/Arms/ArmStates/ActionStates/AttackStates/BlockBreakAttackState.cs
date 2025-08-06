using UnityEngine;

public class BlockBreakAttackState : AttackState
{
    public override void OnEnter()
    {
        type = ActionType.BlockBreakAttack;

        base.OnEnter();

        arm.OnAnimationEnd += stateMachine.Neutral;

        arm.animator.SetTrigger("HeavyAttack");
    }

    public override void OnExit()
    {
        base.OnExit();

        arm.OnAnimationEnd += stateMachine.Neutral;
    }

    protected override void Hit()
    {
        hitData = new HitData(stats.damages, stats.StaggerDuration);
        
        Debug.Log("TAPE");
        targetArm.ReceiveHit(GameManager.Instance.opponentId, arm, hitData);
    }

    protected override void AttackBlocked()
    {
        targetArm.ReceiveTrueHit(GameManager.Instance.opponentId, arm, hitData);
    }
}
