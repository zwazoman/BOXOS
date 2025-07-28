using UnityEngine;

public class OverHeatState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Exhaust");

        arm.OnReceiveHit += DamagingHit;

        stateDuration = PlayerStats.OverheatDuration;
    }

    public override void Update()
    {
        HandleDurationBasedExit();
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= DamagingHit;
    }
}
