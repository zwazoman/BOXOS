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

        arm.OnExhaust += stateMachine.Exhaust;

        //temp
        arm.player.OnKick += stateMachine.Block;
    }

    public override void OnExit()
    {
        base.OnExit();

        arm.OnReceiveHit -= DamagingHit;

        arm.OnExhaust -= stateMachine.Exhaust;
    }

    public override void Update()
    {
        if (!update)
            return;

        //test
        //InputTools.InputAngleEnter(Vector2.right, armInputDelta);

        //attack Prep
        if (InputTools.InputAngleEnter(Vector2.down, armInputDelta))
        {
            StopUpdate();
            stateMachine.AttackPrep();
            return;
        }

        //defense Prep
        if (InputTools.InputAngleEnter(Vector2.left, armInputDelta))
        {
            StopUpdate();
            stateMachine.DefensePrep();
            return;
        }
    }
}
