using System.Globalization;
using UnityEngine;

public class GuardBreakState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("GuardBreak");

        arm.player.UpdateHeat(PlayerStats.GuardBreakHeatCost);

        arm.CheckAnimationCycle();
        arm.OnAnimationCycle += GuardBreakEnd;

        arm.OnSuccessfullGuardBreak += stateMachine.Neutral;

        arm.OnExhaust += stateMachine.OverHeat;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= GuardBreakEnd;

        arm.OnSuccessfullGuardBreak -= stateMachine.Neutral;

        arm.OnExhaust -= stateMachine.OverHeat;
    }

    void GuardBreakEnd()
    {
        stateMachine.Stagger();
    }
}
