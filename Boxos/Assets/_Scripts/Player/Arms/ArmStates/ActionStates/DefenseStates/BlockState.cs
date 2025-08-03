using Unity.VisualScripting;
using UnityEngine;

public class BlockState : DefenseState
{
    public override void OnEnter()
    {
        stateDuration = PlayerStats.BlockWindowDuration;

        base.OnEnter();
        arm.animator.SetTrigger("Block");

        arm.player.UpdateHeat(PlayerStats.BlockHeatCost);

        arm.OnReceiveHit += Block;
        arm.OnReceiveGuardBreak += GuardBroken;

        arm.OnExhaust += stateMachine.OverHeat;
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= Block;
        arm.OnReceiveGuardBreak -= GuardBroken;

        arm.OnExhaust -= stateMachine.OverHeat;
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
