using UnityEngine;

public class ExhaustState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Exhaust");

        arm.OnReceiveHit += FreeHit;
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= FreeHit;
    }
}
