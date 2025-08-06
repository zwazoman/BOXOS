using UnityEngine;

public class DefenseBreakAttack : AttackState
{
    public override void OnEnter()
    {
        type = ActionType.DefenseBreakAttack;

        base.OnEnter();

        arm.animator.SetTrigger("LightAttack");

        arm.OnAnimationEnd += stateMachine.Neutral;

        hitData = new HitData(0, 0);
    }

    public override void OnExit()
    {
        base.OnExit();

        arm.OnAnimationEnd -= stateMachine.Neutral;
    }

    protected override void AttackBlocked()
    {
        hitData = new HitData(stats.damages, stats.StaggerDuration);
        targetArm.ReceiveTrueHit(GameManager.Instance.opponentId, arm, hitData);
    }

    protected override void ParryAttempt(Arm parryingArm)
    {
        if (!isParriable)
            return;

        hitData = new HitData(stats.damages * PlayerStats.ParriedDamageMultiplier, stats.StaggerDuration * PlayerStats.ParriedStaggerTimeMultiplier);
        targetArm.ReceiveTrueHit(GameManager.Instance.opponentId, arm, hitData);
    }

    protected override void Hit()
    {
        targetArm.ReceiveHit(GameManager.Instance.opponentId, arm, hitData);
    }
}
