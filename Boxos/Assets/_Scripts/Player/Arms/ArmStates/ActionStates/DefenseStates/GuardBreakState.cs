using System.Globalization;
using UnityEngine;

public class GuardBreakState : DefenseState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("GuardBreak");

        arm.player.UpdateHeat(PlayerStats.GuardBreakHeatCost);

        arm.OnAnimationEnd += GuardBreakEnd;

        arm.OnSuccessfullGuardBreak += stateMachine.Neutral;

        arm.OnExhaust += stateMachine.OverHeat;
    }

    public override void OnExit()
    {
        arm.OnAnimationEnd -= GuardBreakEnd;

        arm.OnSuccessfullGuardBreak -= stateMachine.Neutral;

        arm.OnExhaust -= stateMachine.OverHeat;
    }

    void GuardBreakEnd()
    {
        stateMachine.Stagger();
    }
}
