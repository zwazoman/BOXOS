using Unity.VisualScripting;
using UnityEngine;

public class BlockState : ArmState
{
    public override void OnEnter()
    {
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

    void Block(Arm attackingArm, AttackStats attackStats)
    {
        Debug.Log("Blocked !");
        attackingArm.Blocked(GameManager.Instance.opponentId);
    }

    void GuardBroken(Arm guardBreakingArm)
    {
        Debug.Log("Guard Broken !");

        stateMachine.Stagger(3);
        guardBreakingArm.SuccessfullGuardbreak(GameManager.Instance.opponentId);
    }

}
