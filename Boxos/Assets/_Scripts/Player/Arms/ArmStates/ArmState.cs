using PurrNet.Packing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ArmState : IPackedAuto
{
    public ArmStateMachine stateMachine;
    public Arm arm;

    public Vector2 armInputDelta;

    protected float exitTimer;

    public virtual void OnEnter()
    {
        exitTimer = 0;
    }

    public virtual void Update() { }

    public virtual void OnExit() { }

   
}
