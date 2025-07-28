using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackState : ArmState
{
    public int attackID;

    bool isParriable = false;

    bool isCancelable = false;

    public override void OnEnter()
    {
        isParriable = false;

        switch (attackID)
        {
            default:
                arm.animator.SetTrigger("LightAttack");
                arm.player.UpdateHeat(PlayerStats.LightAttackHeatCost);
                break;
            case 1:
                arm.animator.SetTrigger("HeavyAttack");
                arm.player.UpdateHeat(PlayerStats.HeavyAttackHeatCost);
                isCancelable = true;
                break;
        }

        arm.CheckAnimationCycle();
        arm.OnAnimationCycle += stateMachine.Neutral;

        arm.OnReceiveHit += DamagingHit;

        arm.OnBlocked += AttackBlocked;
        arm.OnReceiveGuardBreak += AttackParried;
        arm.OnCancel += AttackCanceled;

        arm.OnParryWindow += ParryWindowHandle;
        arm.OnCancelWindow += CancelWindowHandle;

        arm.OnExhaust += stateMachine.OverHeat;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= stateMachine.Neutral;

        arm.OnReceiveHit -= DamagingHit;

        arm.OnBlocked -= AttackBlocked;
        arm.OnReceiveGuardBreak -= AttackParried;

        arm.OnParryWindow -= ParryWindowHandle;

        arm.OnExhaust -= stateMachine.OverHeat;
    }

    void AttackBlocked()
    {
        Debug.Log("Attack blocked !");

        stateMachine.Stagger(1f);

        //surement jouer un son là
    }


    void AttackParried(Arm parryingArm)
    {
        if (!isParriable)
            return;

        Debug.Log("Attack Parried");
        
        switch (attackID)
        {
            default:
                stateMachine.Stagger(PlayerStats.ParriedLightAttackStaggerDuration);
                arm.player.UpdateHeat(PlayerStats.ParriedLightAttackHeatCost);
                break;
            case 1:
                stateMachine.Stagger(PlayerStats.ParriedHeavyAttackStaggerDuration);
                arm.player.UpdateHeat(PlayerStats.ParriedHeavyAttackHeatCost);
                break;
        }
        parryingArm.SuccessfullGuardbreak(GameManager.Instance.opponentId);
    }

    void AttackCanceled()
    {
        arm.player.UpdateHeat(2);
        stateMachine.Neutral();
    }

    void ParryWindowHandle(bool state)
    {
        isParriable = state;
    }

    void CancelWindowHandle(bool state)
    {
        isCancelable = state;
    }
}
