using UnityEngine;

public class ParryState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Parry");
        arm.OnAnimationCycle += stateMachine.Neutral;

        arm.OnReceiveHit += Parry;
    }

    public override void OnExit()
    {
        arm.OnAnimationCycle -= stateMachine.Neutral;

        arm.OnReceiveHit -= Parry;
    }

    void Parry(Arm attackingArm, int attackID)
    {
        Debug.Log("Parry");
    }
}
