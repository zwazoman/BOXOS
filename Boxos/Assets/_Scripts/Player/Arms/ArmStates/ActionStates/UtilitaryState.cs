using UnityEngine;

public class UtilitaryState : ActionState
{
    public override void OnEnter()
    {
        base.OnEnter();

        actionData = stateMachine.utilitaries[type].data;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
