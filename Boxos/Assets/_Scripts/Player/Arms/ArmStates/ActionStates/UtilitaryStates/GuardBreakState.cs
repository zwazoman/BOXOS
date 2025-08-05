using System.Globalization;
using UnityEngine;

public class GuardBreakState : UtilitaryState
{
    public override void OnEnter()
    {
        type = ActionType.GuardBreak;

        base.OnEnter();

        arm.animator.SetTrigger("GuardBreak");

        arm.OnAnimationEnd += GuardBreakEnd;
        arm.OnSuccessfullGuardBreak += stateMachine.Neutral;
    }

    public override void OnExit()
    {
        base.OnExit();
        arm.OnAnimationEnd -= GuardBreakEnd;
        arm.OnSuccessfullGuardBreak -= stateMachine.Neutral;
    }

    void GuardBreakEnd()
    {
        stateMachine.Stagger();
    }
}
