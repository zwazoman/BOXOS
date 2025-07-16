using PurrNet.Packing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ArmState : IPackedAuto
{
    public ArmStateMachine stateMachine;
    public Arm arm;

    public Vector2 stickDelta;


    public virtual void OnEnter() { Debug.Log("coucou"); }

    public virtual void Update() { }

    public virtual void OnExit() { }

    #region Transitions
    protected void Neutral()
    {
        stateMachine.TransitionTo(stateMachine.neutralState);
    }

    protected void Stagger()
    {
        stateMachine.TransitionTo(stateMachine.staggerState);
    }

    protected void Exhaust()
    {
        stateMachine.TransitionTo(stateMachine.exhaustState);
    }

    protected void AttackPrep()
    {
        stateMachine.TransitionTo(stateMachine.attackPrepState);
    }

    protected void Attack()
    {
        stateMachine.TransitionTo(stateMachine.attackState);
    }

    protected void Block()
    {
        stateMachine.TransitionTo(stateMachine.blockState);
    }

    protected void Parry()
    {
        stateMachine.TransitionTo(stateMachine.parryState);
    }

    #endregion
}
