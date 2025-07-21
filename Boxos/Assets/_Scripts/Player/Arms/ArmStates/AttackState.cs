using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackState : ArmState
{
    public int attackID;

    bool isCancelable = false;

    public override void OnEnter()
    {
        switch (attackID)
        {
            default:
                arm.animator.SetTrigger("LightAttack");
                break;
            case 1:
                arm.animator.SetTrigger("HeavyAttack");
                isCancelable = true;
                break;
        }

        arm.CheckAnimationCycle();
        arm.OnAnimationCycle += stateMachine.Neutral;

        arm.OnReceiveHit += FreeHit;

        arm.OnBlocked += AttackBlocked;
        arm.OnParried += AttackParried;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= stateMachine.Neutral;

        arm.OnReceiveHit -= FreeHit;

        arm.OnBlocked -= AttackBlocked;
        arm.OnParried -= AttackParried;
    }

    void AttackBlocked()
    {
        Debug.Log("Attack blocked !");
    }

    void AttackParried()
    {
        Debug.Log("AttackParried");
    }
}
