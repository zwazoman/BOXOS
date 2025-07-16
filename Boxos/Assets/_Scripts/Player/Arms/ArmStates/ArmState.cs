using PurrNet.Packing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ArmState : IPackedAuto
{
    public ArmStateMachine stateMachine;
    public Arm arm;

    public Vector2 armInputDelta;


    public virtual void OnEnter() { Debug.Log("coucou"); }

    public virtual void Update() { }

    public virtual void OnExit() { }

   
}
