using UnityEngine;

public class DefensePrepState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("DefensePrep");

        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust += stateMachine.Exhaust;
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= DamagingHit;

        arm.OnExhaust -= stateMachine.Exhaust;
    }

    public override void Update()
    {
        if (!update)
            return;

        //blockHandle
        if(InputTools.InputAngleEnter(Vector2.up, armInputDelta))
        {
            stateMachine.Block();
            StopUpdate();
            return;
        }

        //GB handle
        if (InputTools.InputAngleEnter(Vector2.right, armInputDelta))
        {
            stateMachine.GuardBreak();
            StopUpdate();
            return;
        }

        //exit conditions
        if (InputTools.InputAngleExit(Vector2.left, armInputDelta))
        {
            exitTimer += Time.deltaTime;

            if (exitTimer >= PlayerStats.InputExitTime)
            {
                stateMachine.Neutral();
                StopUpdate();
            }
        }
    }
}
