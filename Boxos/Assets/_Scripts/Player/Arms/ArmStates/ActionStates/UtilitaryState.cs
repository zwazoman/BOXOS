using UnityEngine;

public class UtilitaryState : ActionState
{
    public override void OnEnter()
    {
        actionData = stateMachine.utilitaries[type].data;
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
