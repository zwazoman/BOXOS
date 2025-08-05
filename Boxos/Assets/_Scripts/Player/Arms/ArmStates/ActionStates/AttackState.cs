using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackState : ActionState
{
    protected AttackStats stats;
    protected HitData hitData;

    protected bool isParriable;

    protected bool isCancelable;

    public override void OnEnter()
    {
        base.OnEnter();

        actionData = stateMachine.attacks[type].data;
        stats = stateMachine.attacks[type].data.stats;

        isParriable = false;
        isCancelable = false;

        arm.OnHit += Hit;

        arm.OnBlocked += AttackBlocked;
        arm.OnReceiveGuardBreak += AttackParried;
        arm.OnCancel += AttackCanceled;

        arm.OnParryWindow += ParryWindowHandle;
        arm.OnCancelWindow += CancelWindowHandle;

        Debug.Log("attack entered");
    }

    public override void OnExit()
    {
        arm.OnHit -= Hit;

        arm.OnBlocked -= AttackBlocked;
        arm.OnReceiveGuardBreak -= AttackParried;
        arm.OnCancel -= AttackCanceled;

        arm.OnParryWindow -= ParryWindowHandle;
    }

    protected virtual void Hit()
    {
        hitData = new HitData(stats.damages, stats.StaggerDuration);
        Debug.Log("TAPE");
        Arm targetArm = GameManager.Instance.opponent.GetOpposedArmBySide(arm.side);

        targetArm.ReceiveHit(GameManager.Instance.opponentId, arm, hitData);
    }

    protected virtual void AttackBlocked()
    {
        Debug.Log("Attack blocked !");
        //surement jouer un son là

        stateMachine.Stagger(stats.blockedStaggerTime);
        arm.player.UpdateHeat(stats.blockedHeatCost);

    }


    protected virtual void AttackParried(Arm parryingArm)
    {
        if (!isParriable)
            return;

        Debug.Log("parried");

        stateMachine.Stagger(stats.parriedStaggerTime);
        arm.player.UpdateHeat(stats.parriedHeatCost);

        parryingArm.SuccessfullGuardbreak(GameManager.Instance.opponentId);
    }

    protected virtual void AttackCanceled()
    {
        Debug.Log("IL A ESSAYE DE CANCEL");

        if (!isCancelable)
            return;

        stateMachine.Neutral();
    }

    protected void ParryWindowHandle(bool state)
    {
        isParriable = state;
    }

    protected void CancelWindowHandle(bool state)
    {
        isCancelable = state;
    }
}
