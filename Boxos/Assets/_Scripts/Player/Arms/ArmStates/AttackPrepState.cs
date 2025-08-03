using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackPrepState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("PrepAttack");

        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust += stateMachine.OverHeat;

        foreach(AttackTruc attack in stateMachine.attacks.Values)
        {
            arm.inputs.actionDatas.Add(attack.data);
            attack.data.inputs.OnPerformed += TransitionWithType;
        }
    }

    public override void OnExit()
    {
        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust -= stateMachine.OverHeat;

        foreach (AttackTruc attack in stateMachine.attacks.Values)
        {
            attack.data.inputs.OnPerformed -= TransitionWithType;
        }

        arm.inputs.ClearArmInputs();
    }

    public override void Update()
    {

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
