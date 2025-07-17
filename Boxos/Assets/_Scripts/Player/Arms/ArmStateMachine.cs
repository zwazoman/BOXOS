using Mono.Cecil.Cil;
using PurrNet;
using UnityEngine;

public class ArmStateMachine : NetworkIdentity
{
    [SerializeField] Arm _arm;

    ArmState currentState;

    #region States
    public NeutralState neutralState = new();
    public StaggerState staggerState = new();
    public ExhaustState exhaustState = new();
    public AttackPrepState attackPrepState = new();
    public AttackState attackState = new();
    public BlockState blockState = new();
    public ParryState parryState = new();
    #endregion

    private void Awake()
    {
        if (_arm == null)
            TryGetComponent(out _arm);
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();

        Neutral();
    }

    public void TransitionTo(ArmState state)
    {
        if(currentState != null)
        {
            if (state == currentState)
                return;
            currentState.OnExit();
        }

        currentState = state;
        currentState.stateMachine = this;
        currentState.arm = _arm;

        currentState.OnEnter();
    }

    void Update()
    {
        if (!isOwner)
            return;

        currentState.armInputDelta = _arm.player.GetStickVector(_arm.side);
        currentState.Update();
    }

    public bool CheckCurrentState(ArmState state)
    {
        if (currentState.GetType() == state.GetType())
            return true;
        return false;
    }

    #region Transitions

    [ObserversRpc]
    public void Neutral()
    {
        print("Neutral");
        TransitionTo(neutralState);
    }

    [ObserversRpc]
    public void Stagger()
    {
        print("Stagger");
        TransitionTo(staggerState);
    }

    [ObserversRpc]
    public void Exhaust()
    {
        print("Exhaust");
        TransitionTo(exhaustState);
    }

    [ObserversRpc]
    public void AttackPrep()
    {
        print("AttackPrep");
        TransitionTo(attackPrepState);
    }

    [ObserversRpc]
    public void Attack(int attackID)
    {
        print("Attack");
        attackState.attackID = attackID;
        TransitionTo(attackState);
    }

    [ObserversRpc]
    public void Block()
    {
        print("Block");
        TransitionTo(blockState);
    }

    [ObserversRpc]
    public void Parry()
    {
        print("Parry");
        TransitionTo(parryState);
    }

    #endregion

}
