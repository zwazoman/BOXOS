using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackPrepState : ArmState
{
    bool _circularInput = false;

    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("PrepAttack");
    }

    public override void OnExit()
    {
        _circularInput = false;
    }

    public override void Update()
    {
        //heavy attack handle
        if(InputTools.CheckInputAngleEnter(0, armInputDelta))
        {
            _circularInput = true;
        }
        if (_circularInput && InputTools.CheckInputAngleEnter(90, armInputDelta))
        {
            stateMachine.Attack(1);
            return;
        }

        //light attack handle
        if (InputTools.CheckInputAngleEnter(90, armInputDelta))
        {
            stateMachine.Attack(0);
            return;
        }


        //Exit Conditions
        if (InputTools.CheckInputAngleExit(-90, armInputDelta))
        {
            exitTimer += Time.deltaTime;

            if (exitTimer >= PlayerStats.InputExitTime)
                stateMachine.Neutral();
        }
    }
}
