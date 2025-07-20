using System.Globalization;
using UnityEngine;

public class GuardBreakState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Parry");
        arm.OnAnimationCycle += () => stateMachine.Stagger();

        arm.OnSuccessfullGuardBreak += stateMachine.Neutral;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= () => stateMachine.Stagger();

        arm.OnSuccessfullGuardBreak -= stateMachine.Neutral;
    }

    public void GuardBreak()
    {
        if (!arm.isOwner)
            return;

        Arm targetArm = GameManager.Instance.opponent.GetOpposedArm(arm.side);
        targetArm.GuardBroken(arm);
    }
}
