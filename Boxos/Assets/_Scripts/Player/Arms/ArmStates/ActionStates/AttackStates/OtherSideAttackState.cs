using UnityEngine;

public class OtherSideAttackState : AttackState
{
    public override void OnEnter()
    {
        type = ActionType.OtherSideAttack;

        base.OnEnter();

        arm.OnAnimationEnd += stateMachine.Neutral;

        arm.animator.SetTrigger("HeavyAttack");
    }

    public override void OnExit()
    {
        base.OnExit();

        arm.OnAnimationEnd -= stateMachine.Neutral;
    }

    protected override void Hit()
    {
        Arm targetArm = GameManager.Instance.opponent.GetArmBySide(arm.side);

        targetArm.ReceiveHit(GameManager.Instance.opponentId, arm, hitData);
    }
}
