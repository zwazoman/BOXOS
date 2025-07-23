using Unity.VisualScripting;
using UnityEngine;

public class BlockState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("Block");

        arm.player.UpdateStamina(-PlayerStats.BlockStaminaCost);

        arm.OnReceiveHit += Block;
        arm.OnGuardBroken += GuardBroken;

        arm.OnExhaust += stateMachine.Exhaust;
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= Block;
        arm.OnGuardBroken -= GuardBroken;

        arm.OnExhaust -= stateMachine.Exhaust;
    }

    public override void Update()
    {
        if (!update)
            return;

        HandleDurationBasedExit();
    }

    void Block(Arm attackingArm, int attackID)
    {
        Debug.Log("Block");
        attackingArm.Blocked();
    }

    void GuardBroken(Arm guardBreakingArm)
    {
        stateMachine.Stagger();
    }

}
