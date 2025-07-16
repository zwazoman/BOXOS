using UnityEngine;

public class AttackPrepState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("PrepAttack");
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        //Exit Conditions
        if (InputTools.CheckInputAngleExit(-90, armInputDelta))
            stateMachine.Neutral();
    }
}
