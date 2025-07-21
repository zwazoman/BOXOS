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

        Debug.Log(stateDuration + " " + exitTimer);
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {
        HandleDurationBasedExit();
    }


}
