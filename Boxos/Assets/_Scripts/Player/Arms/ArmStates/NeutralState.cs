using PurrNet.Packing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class NeutralState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();

        arm.animator.SetTrigger("Idle");

        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust += stateMachine.OverHeat;

        //temp
        arm.player.OnKick += stateMachine.Block;
    }

    public override void OnExit()
    {
        base.OnExit();

        arm.OnReceiveHit -= DamagingHit;

        arm.OnExhaust -= stateMachine.OverHeat;
    }

    public override void Update()
    {
        //attack Prep
        if (InputTools.InputAngleEnter(Vector2.down, armInputDelta))
        {
            stateMachine.AttackPrep();
            return;
        }

        //defense Prep
        if (InputTools.InputAngleEnter(Vector2.left, armInputDelta))
        {
            stateMachine.DefensePrep();
            return;
        }
    }
}
