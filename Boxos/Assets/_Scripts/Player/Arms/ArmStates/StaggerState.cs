using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class StaggerState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();

        arm.animator.SetTrigger("Stagger");

        if (stateDuration == 0)
            stateDuration = 2;

        arm.OnExhaust += stateMachine.OverHeat;
    }

    public override void OnExit()
    {
        arm.OnExhaust -= stateMachine.OverHeat;
    }

    public override void Update()
    {
        HandleDurationBasedExit();
    }


}
