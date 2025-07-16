using PurrNet.Packing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class NeutralState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Idle");
    }

    public override void Update()
    {
        float armDegInputValue = InputTools.ArmInputValue(armInputDelta);
        float armInputDistanceToNeutral = InputTools.DistanceToNeutral(armInputDelta);

        Debug.Log(armDegInputValue + "  " + armInputDistanceToNeutral);

        if (armDegInputValue > -90 - PlayerStats.ArmInputMargin && armDegInputValue < -90 + PlayerStats.ArmInputMargin && armInputDistanceToNeutral > .8f)
        {
            stateMachine.AttackPrep();
        }
    }
}
