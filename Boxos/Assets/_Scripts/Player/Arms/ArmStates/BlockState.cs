using UnityEngine;

public class BlockState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("Block");
    }

    public override void Update()
    {
        if (InputTools.CheckInputAngleEnter(0, armInputDelta))
            stateMachine.Parry();

        //exit conditions
        if (InputTools.CheckInputAngleExit(180, armInputDelta))
        {
            exitTimer += Time.deltaTime;

            if(exitTimer >= PlayerStats.InputExitTime)
                stateMachine.Neutral();
        }
            
    }
}
