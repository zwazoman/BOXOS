using UnityEngine;

public class BlockState : ArmState
{
    public override void OnEnter()
    {
        Debug.Log("Block State");
        arm.animator.SetTrigger("Block");
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.Neutral();
    }
}
