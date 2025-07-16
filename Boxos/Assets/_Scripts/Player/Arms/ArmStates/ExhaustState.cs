using UnityEngine;

public class ExhaustState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Exhaust");
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
