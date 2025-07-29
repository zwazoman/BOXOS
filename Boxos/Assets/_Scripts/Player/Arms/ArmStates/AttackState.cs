using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackState : ArmState
{
    protected AttackStats? stats;

    protected AttackType type;

    protected bool isParriable;

    protected bool isCancelable;

    public override void OnEnter()
    {
        if (stats.HasValue == false)
            stats = stateMachine.attacks[type];

        isParriable = false;
        isCancelable = false;

        arm.OnHit += Hit;

        arm.OnReceiveHit += DamagingHit;

        arm.OnBlocked += AttackBlocked;
        arm.OnReceiveGuardBreak += AttackParried;
        arm.OnCancel += AttackCanceled;

        arm.OnParryWindow += ParryWindowHandle;
        arm.OnCancelWindow += CancelWindowHandle;

        arm.OnExhaust += stateMachine.OverHeat;

        Debug.Log("attack entered");
    }

    public override void OnExit()
    {
        arm.OnHit -= Hit;

        arm.OnReceiveHit -= DamagingHit;

        arm.OnBlocked -= AttackBlocked;
        arm.OnReceiveGuardBreak -= AttackParried;
        arm.OnCancel -= AttackCanceled;

        arm.OnParryWindow -= ParryWindowHandle;

        arm.OnExhaust -= stateMachine.OverHeat;
    }

    protected virtual void Hit()
    {
        Debug.Log("TAPE");

        Arm targetArm = GameManager.Instance.opponent.GetOpposedArm(arm.side);

        targetArm.ReceiveHit(GameManager.Instance.opponentId, arm, stats.Value);
    }

    protected virtual void AttackBlocked()
    {
        Debug.Log("Attack blocked !");
        //surement jouer un son là

        stateMachine.Stagger(stats.Value.blockedStaggerTime);
        arm.player.UpdateHeat(stats.Value.blockedHeatCost);

    }


    protected virtual void AttackParried(Arm parryingArm)
    {
        if (!isParriable)
            return;

        Debug.Log("parried");

        stateMachine.Stagger(stats.Value.parriedStaggerTime);
        arm.player.UpdateHeat(stats.Value.parriedHeatCost);

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
