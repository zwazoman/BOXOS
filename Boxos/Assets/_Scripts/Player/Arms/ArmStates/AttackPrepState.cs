using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackPrepState : ArmState
{
    bool _circularInput = false;

    ArmInput test = new();
    ArmInput test2 = new();
    ArmInput test3 = new();

    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("PrepAttack");

        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust += stateMachine.OverHeat;

        if (_circularInput)
        {
            arm.inputs.armInputs.Add(test);
            arm.inputs.armInputs.Add(test3);
            arm.inputs.armInputs.Add(test2);
            return;
        }

        test2.directions.Add(Vector2.up);

        test.directions.Add(Vector2.right);
        test.directions.Add(Vector2.up);

        test3.directions.Add(Vector2.right);
        test3.directions.Add(Vector2.up);
        test3.directions.Add(Vector2.left);

        test.OnPerformed += () => stateMachine.TransitionTo(stateMachine.heavyAttackState);
        test2.OnPerformed += () => stateMachine.TransitionTo(stateMachine.lightAttackState);
        test3.OnPerformed += () => stateMachine.GuardBreak();

        arm.inputs.armInputs.Add(test);
        arm.inputs.armInputs.Add(test3);
        arm.inputs.armInputs.Add(test2);

        _circularInput = true;
    }

    public override void OnExit()
    {
        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust -= stateMachine.OverHeat;

        arm.inputs.ClearArmInputs();
    }

    public override void Update()
    {
        ////heavy attack handle
        //if (InputTools.InputAngle(Vector2.right, armInputDelta))
        //{
        //    _circularInput = true;
        //}
        //if (_circularInput && InputTools.InputAngle(Vector2.up, armInputDelta))
        //{
        //    stateMachine.TransitionTo(stateMachine.heavyAttackState);
        //    return;
        //}

        ////light attack handle
        //if (InputTools.InputAngle(Vector2.up, armInputDelta))
        //{
        //    stateMachine.TransitionTo(stateMachine.lightAttackState);
        //    return;
        //}


        //Exit Conditions
        if (InputTools.InputAngle(Vector2.down, armInputDelta, false))
        {
            exitTimer += Time.deltaTime;

            if (exitTimer >= PlayerStats.InputExitTime)
            {
                stateMachine.Neutral();
            }
        }
    }
}
