using System.ComponentModel;
using UnityEngine;

public class RecoveryState : UtilitaryState, ITickingState
{
    float timer;

    public override void OnEnter()
    {
        type = ActionType.Recovery;
        base.OnEnter();

        arm.animator.SetTrigger("Exhaust");
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if(timer >= PlayerStats.RecoveryTimeOffset)
        {
            arm.player.UpdateHeat(-1);
        }

        if(InputTools.InputAngle(stateMachine.attacks[type].data.inputs.directions[stateMachine.attacks[type].data.inputs.directions.Count - 1], armInputDelta, false))
        {
            stateMachine.Neutral();
        }
    }
}
