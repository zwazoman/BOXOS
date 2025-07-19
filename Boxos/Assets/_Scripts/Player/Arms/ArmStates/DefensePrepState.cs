using UnityEngine;

public class DefensePrepState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("Block");

        arm.OnReceiveHit += FreeHit;
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= FreeHit;
    }

    public override void Update()
    {
        if (!update)
            return;

        //blockHandle
        if(InputTools.CheckInputAngleEnter(90, armInputDelta))
        {
            stateMachine.Block();
            StopUpdate();
            return;
        }

        //parry handle
        if (InputTools.CheckInputAngleEnter(0, armInputDelta))
        {
            stateMachine.Parry();
            StopUpdate();
            return;
        }

        //exit conditions
        if (InputTools.CheckInputAngleExit(180, armInputDelta))
        {
            exitTimer += Time.deltaTime;

            if (exitTimer >= PlayerStats.InputExitTime)
            {
                StopUpdate();
                stateMachine.Neutral();
            }
        }
    }
}
