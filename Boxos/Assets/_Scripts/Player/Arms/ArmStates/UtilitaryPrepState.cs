using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class UtilitaryPrepState : ArmState
{
    public override void OnEnter()
    {
        base.OnEnter();
        arm.animator.SetTrigger("DefensePrep");

        arm.OnReceiveHit += DamagingHit;

        arm.OnExhaust += stateMachine.OverHeat;

        foreach (UtilitaryTruc utilitary in stateMachine.utilitaries.Values)
        {
            arm.inputs.actionDatas.Add(utilitary.data);
            utilitary.data.inputs.OnPerformed += TransitionWithType;
        }
    }

    public override void OnExit()
    {
        arm.OnReceiveHit -= DamagingHit;

        arm.OnExhaust -= stateMachine.OverHeat;

        foreach (UtilitaryTruc utilitaries in stateMachine.utilitaries.Values)
        {
            utilitaries.data.inputs.OnPerformed -= TransitionWithType;
        }
        arm.inputs.ClearArmInputs();
    }

    public override void Update()
    {
        //exit conditions
        if (InputTools.InputAngle(Vector2.left, armInputDelta, false))
        {
            exitTimer += Time.deltaTime;

            if (exitTimer >= PlayerStats.InputExitTime)
            {
                stateMachine.Neutral();
            }
        }
    }
}
