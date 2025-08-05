using UnityEngine;

public class ActionState : ArmState
{
    protected ActionData actionData;
    protected ActionType type;

    public override void OnEnter()
    {
        base.OnEnter();

        arm.OnExhaust += stateMachine.OverHeat;

        arm.player.UpdateHeat(actionData.heatCost);

        arm.OnReceiveHit += DamagingHit;
    }

    public override void OnExit()
    {
        base.OnExit();

        arm.OnReceiveHit -= DamagingHit;
        arm.OnExhaust -= stateMachine.OverHeat;
    }
}
