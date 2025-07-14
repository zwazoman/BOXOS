using UnityEngine;

public abstract class ArmState
{
    public ArmStateMachine stateMachine;
    public Arm _arm;


    public abstract void OnEnter();

    public abstract void Update();

    public abstract void OnExit();

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
