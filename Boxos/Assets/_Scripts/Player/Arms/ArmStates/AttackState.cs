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
                arm.player.UpdateStamina(-PlayerStats.LightAttackStaminaCost);
                break;
            case 1:
                arm.animator.SetTrigger("HeavyAttack");
                arm.player.UpdateStamina(-PlayerStats.HeavyAttackStaminaCost);
                isCancelable = true;
                break;
        }

        arm.CheckAnimationCycle();
        arm.OnAnimationCycle += stateMachine.Neutral;

        arm.OnReceiveHit += DamagingHit;

        arm.OnBlocked += AttackBlocked;
        arm.OnReceiveGuardBreak += AttackParried;

        arm.OnParryWindow += ParryWindowHandle;

        arm.OnExhaust += stateMachine.Exhaust;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= stateMachine.Neutral;

        arm.OnReceiveHit -= DamagingHit;

        arm.OnBlocked -= AttackBlocked;
        arm.OnReceiveGuardBreak -= AttackParried;

        arm.OnParryWindow -= ParryWindowHandle;

        arm.OnExhaust -= stateMachine.Exhaust;
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
                arm.player.UpdateStamina(-PlayerStats.ParriedLightAttackStaminaCost);
                break;
            case 1:
                stateMachine.Stagger(PlayerStats.ParriedHeavyAttackStaggerDuration);
                arm.player.UpdateStamina(-PlayerStats.ParriedHeavyAttackStaminaCost);
                break;
        }
        parryingArm.SuccessfullGuardbreak(GameManager.Instance.opponentId);

    }

    void ParryWindowHandle(bool state)
    {
        isParriable = state;
    }
}
