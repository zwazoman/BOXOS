using UnityEngine;

public class DefensePrepState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("DefensePrep");

        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust += stateMachine.OverHeat;
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= DamagingHit;

        arm.OnExhaust -= stateMachine.OverHeat;
    }

    public override void Update()
    {
        //blockHandle
        if(InputTools.InputAngle(Vector2.up, armInputDelta))
        {
            stateMachine.Block();
            return;
        }

        //GB handle
        if (InputTools.InputAngle(Vector2.right, armInputDelta))
        {
            stateMachine.GuardBreak();
            return;
        }

        //exit conditions
        if (InputTools.InputAngle(Vector2.left, armInputDelta, false))
        {
            exitTimer += Time.deltaTime;

            if (exitTimer >= PlayerStats.InputExitTime)
            {
                stateMachine.Neutral();
            }
        }
    }
}
