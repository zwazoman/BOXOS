using System.Globalization;
using UnityEngine;

public class GuardBreakState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("GuardBreak");

        arm.CheckAnimationCycle();
        arm.OnAnimationCycle += GuardBreakEnd;

        arm.OnSuccessfullGuardBreak += stateMachine.Neutral;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= GuardBreakEnd;

        arm.OnSuccessfullGuardBreak -= stateMachine.Neutral;
    }

    void GuardBreakEnd()
    {
        stateMachine.Stagger();
        Debug.Log("Guardbreak end");
    }

    public void GuardBreak()
    {
        if (!arm.isOwner)
            return;

        Arm targetArm = GameManager.Instance.opponent.GetOpposedArm(arm.side);
        targetArm.GuardBroken(arm);
    }
}
