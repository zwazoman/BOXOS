using System.Globalization;
using UnityEngine;

public class GuardBreakState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("GuardBreak");

        arm.player.UpdateStamina(-PlayerStats.GuardBreakStaminaCost);

        arm.CheckAnimationCycle();
        arm.OnAnimationCycle += GuardBreakEnd;

        arm.OnSuccessfullGuardBreak += stateMachine.Neutral;

        arm.OnExhaust += stateMachine.Exhaust;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= GuardBreakEnd;

        arm.OnSuccessfullGuardBreak -= stateMachine.Neutral;

        arm.OnExhaust -= stateMachine.Exhaust;
    }

    void GuardBreakEnd()
    {
        stateMachine.Stagger();
    }
}
