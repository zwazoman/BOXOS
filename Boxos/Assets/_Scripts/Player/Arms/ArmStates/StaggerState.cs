using UnityEngine;

public class StaggerState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Stagger");
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
