using Unity.VisualScripting;
using UnityEngine;

public class BlockState : UtilitaryState
{
    public override void OnEnter()
    {
        stateDuration = PlayerStats.BlockWindowDuration;

        type = ActionType.Block;

        base.OnEnter();
        arm.animator.SetTrigger("Block");

        arm.OnReceiveHit += Block;
        arm.OnReceiveGuardBreak += GuardBroken;
    }

    public override void OnExit()
    {
        base.OnExit();
        arm.OnReceiveHit -= Block;
        arm.OnReceiveGuardBreak -= GuardBroken;
    }

    public override void Update()
    {
        HandleDurationBasedExit();
    }

    void Block(Arm attackingArm, HitData hitData)
    {
        if (!hitData.isUnblockable)
            DamagingHit(attackingArm, hitData);

        arm.player.UpdateHeat(hitData.blockHeatCost);

        attackingArm.Blocked(GameManager.Instance.opponentId);
    }

    void GuardBroken(Arm guardBreakingArm)
    {
        Debug.Log("Guard Broken !");

        stateMachine.Stagger(3);
        guardBreakingArm.SuccessfullGuardbreak(GameManager.Instance.opponentId);
    }

}
