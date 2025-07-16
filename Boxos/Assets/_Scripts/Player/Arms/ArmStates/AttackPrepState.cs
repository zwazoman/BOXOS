using UnityEngine;

public class AttackPrepState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("PrepAttack");
    }

    public override void Update()
    {
        if (InputTools.CheckInputAngleEnter(90, armInputDelta))
            stateMachine.Attack(0);

        //Exit Conditions
        if (InputTools.CheckInputAngleExit(-90, armInputDelta))
        {
            exitTimer += Time.deltaTime;

            if (exitTimer >= PlayerStats.InputExitTime)
                stateMachine.Neutral();
        }
    }
}
