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
        if (InputTools.DistanceToNeutral(armInputDelta) <= .2)
            stateMachine.Neutral();
    }
}
