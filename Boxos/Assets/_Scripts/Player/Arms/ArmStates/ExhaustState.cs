using UnityEngine;

public class ExhaustState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Exhaust");

        arm.OnReceiveHit += DamagingHit;
    }

    public override void Update()
    {
        base.Update();

        Debug.Log("Bien exhaust");
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= DamagingHit;
    }
}
