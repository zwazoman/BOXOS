using PurrNet.Packing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class NeutralState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();

        arm.animator.SetTrigger("Idle");
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Update()
    {
        if (!update)
            return;

        //attack prep
        if (InputTools.CheckInputAngleEnter(-90, armInputDelta))
        {
            StopUpdate();
            Debug.Log(update);
            stateMachine.AttackPrep();
            return;
        }

        //Block
        if (InputTools.CheckInputAngleEnter(180, armInputDelta) || InputTools.CheckInputAngleEnter(-180, armInputDelta))
        {
            StopUpdate();
            stateMachine.Block();
            return;
        }
    }
}
