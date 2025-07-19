using UnityEngine;

public class BlockState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("Block");

        arm.OnReceiveHit += Block;

        arm.player.OnKick += stateMachine.Parry;
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= Block;
    }

    public override void Update()
    {
        if (!update)
            return;

        if (InputTools.CheckInputAngleEnter(0, armInputDelta))
        {
            stateMachine.Parry();
            StopUpdate();
            return;
        }

        //exit conditions
        //if (InputTools.CheckInputAngleExit(180, armInputDelta))
        //{
        //    exitTimer += Time.deltaTime;

        //    if(exitTimer >= PlayerStats.InputExitTime)
        //    {
        //        StopUpdate();
        //        stateMachine.Neutral();
        //    }
        //}
    }

    void Block(Arm attackingArm, int attackID)
    {
        Debug.Log("Block");
        attackingArm.Blocked();
    }

}
