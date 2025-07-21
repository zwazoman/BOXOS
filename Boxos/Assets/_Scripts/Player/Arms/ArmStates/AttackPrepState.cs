using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackPrepState : ArmState
{
    bool _circularInput = false;

    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("PrepAttack");

        arm.OnReceiveHit += FreeHit;
    }

    public override void OnExit()
    {
        _circularInput = false;
        arm.OnReceiveHit += FreeHit;
    }

    public override void Update()
    {
        if (!update)
            return;

        //heavy attack handle
        if (InputTools.InputAngleEnter(Vector2.right, armInputDelta))
        {
            _circularInput = true;
        }
        if (_circularInput && InputTools.InputAngleEnter(Vector2.up, armInputDelta))
        {
            stateMachine.Attack(1);
            StopUpdate();
            return;
        }

        //light attack handle
        if (InputTools.InputAngleEnter(Vector2.up, armInputDelta))
        {
            stateMachine.Attack(0);
            StopUpdate();
            return;
        }


        //Exit Conditions
        if (InputTools.InputAngleExit(Vector2.down, armInputDelta))
        {
            exitTimer += Time.deltaTime;

            if (exitTimer >= PlayerStats.InputExitTime)
            {
                StopUpdate();
                stateMachine.Neutral();
            }
        }
    }
}
