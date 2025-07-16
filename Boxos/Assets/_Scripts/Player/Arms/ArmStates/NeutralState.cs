using PurrNet.Packing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class NeutralState : ArmState
{
    public override void OnEnter()
    {
        arm.animator.SetTrigger("Idle");
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.Block();
        }
    }
}
