using UnityEngine;

public class BlockBreakAttackState : AttackState
{
    public override void OnEnter()
    {
        type = ActionType.BlockBreakAttack;

        base.OnEnter();

        hitData.isUnblockable = false;

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
        hitData = new HitData(stats.Value.damage, stats.Value.StaggerDuration);
        hitData.isUnblockable = true;
        
        Debug.Log("TAPE");
        Arm targetArm = GameManager.Instance.opponent.GetOpposedArmBySide(arm.side);

        targetArm.ReceiveHit(GameManager.Instance.opponentId, arm, hitData);
    }
}
