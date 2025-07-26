using UnityEngine;

public class ExhaustState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Exhaust");

        arm.OnReceiveHit += DamagingHit;

        stateDuration = PlayerStats.ExhaustDuration;
    }

    public override void Update()
    {
        if (!update)
            return;

        HandleDurationBasedExit();
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= DamagingHit;
    }
}
