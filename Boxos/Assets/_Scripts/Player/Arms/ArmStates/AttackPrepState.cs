using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackPrepState : ArmState
{
    bool _circularInput = false;

    ArmInput test = new();

    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("PrepAttack");

        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust += stateMachine.OverHeat;

        test.directions.Add(Vector2.left);
        test.directions.Add(Vector2.right);

        test.OnPerformed += () => stateMachine.TransitionTo(stateMachine.heavyAttackState);

        arm.inputs.armInputs.Add(test);
    }

    public override void OnExit()
    {
        _circularInput = false;
        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust -= stateMachine.OverHeat;

        arm.inputs.ClearArmInputs();
    }

    public override void Update()
    {
        //heavy attack handle
        if (InputTools.InputAngle(Vector2.right, armInputDelta))
        {
            _circularInput = true;
        }
        if (_circularInput && InputTools.InputAngle(Vector2.up, armInputDelta))
        {
            stateMachine.TransitionTo(stateMachine.heavyAttackState);
            return;
        }

        //light attack handle
        if (InputTools.InputAngle(Vector2.up, armInputDelta))
        {
            stateMachine.TransitionTo(stateMachine.lightAttackState);
            return;
        }


        //Exit Conditions
        if (InputTools.InputAngle(Vector2.down, armInputDelta, false))
        {
            exitTimer += Time.deltaTime;

            if (exitTimer >= /*PlayerStats.InputExitTime*/1)
            {
                stateMachine.Neutral();
            }
        }
    }
}
