using UnityEngine;

public class ParryState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Parry");
        arm.OnAnimationCycle += stateMachine.Neutral;

        arm.OnReceiveHit += Parry;

        arm.OnExhaust += stateMachine.Exhaust;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= stateMachine.Neutral;

        arm.OnReceiveHit -= Parry;

        arm.OnExhaust -= stateMachine.Exhaust;
    }

    void Parry(Arm attackingArm, int attackID)
    {
        Debug.Log("Parry");
        arm.Parried();
    }
}
