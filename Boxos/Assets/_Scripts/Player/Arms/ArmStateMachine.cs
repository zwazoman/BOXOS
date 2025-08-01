using NUnit.Framework;
using PurrNet;
using UnityEngine;
using System.Collections.Generic;

public class ArmStateMachine : NetworkIdentity
{
    public Dictionary<AttackType, AttackStats> attacks = new();
    [SerializeField] List<AttacksScriptable> attacksScriptables = new();

    [SerializeField] Arm _arm;

    ArmState currentState;

    #region States
    public NeutralState neutralState = new();
    public StaggerState staggerState = new();
    public OverHeatState overHeatState = new();
    public AttackPrepState attackPrepState = new();
    public AttackState attackState = new();
    public DefensePrepState defensePrepState = new();
    public BlockState blockState = new();
    public GuardBreakState guardBreakState = new();

    //attacks
    public LightAttackState lightAttackState = new();
    public HeavyAttackState heavyAttackState = new();

    #endregion

    private void Awake()
    {
        if (_arm == null)
            TryGetComponent(out _arm);
    }
    protected override void OnSpawned()
    {
        base.OnSpawned();

        if (!isOwner)
            return;

        foreach (AttacksScriptable truc in attacksScriptables)
        {
            attacks.Add(truc.attackType, truc.stats);
        }

        if (GameManager.Instance.opponentId == PlayerID.Server)
            GameManager.Instance.OnPlayerSpawned += Neutral;
        else
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
        if (!isOwner || currentState == null)
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

    //[ObserversRpc]
    public void Neutral()
    {
        if(isOwner) print("Neutral" + " " + owner);

        TransitionTo(neutralState);
    }

    //[ObserversRpc]
    public void Stagger(float duration = 0)
    {
        if (isOwner) print("Stagger" + " " + owner);

        staggerState.stateDuration = duration;
        TransitionTo(staggerState);
    }

    //[ObserversRpc]
    public void OverHeat()
    {
        if (isOwner) print("Overheat" + " " + owner);

        TransitionTo(overHeatState);
    }

    //[ObserversRpc]
    public void AttackPrep()
    {
        if (isOwner) print("AttackPrep" + " " + owner);

        TransitionTo(attackPrepState);
    }

    //[ObserversRpc]
    public void Attack(int attackId)
    {
        if (isOwner) print("Attack" + " " + owner);
        TransitionTo(attackState);
    }

    //[ObserversRpc]
    public void DefensePrep()
    {
        if (isOwner) print("Defense Prep" + " " + owner);

        TransitionTo(defensePrepState);
    }

    //[ObserversRpc]
    public void Block()
    {
        if (isOwner) print("Block" + " " + owner);

        blockState.stateDuration = PlayerStats.BlockWindowDuration;
        TransitionTo(blockState);
    }

    //[ObserversRpc]
    public void GuardBreak()
    {
        if (isOwner) print("Guard Break" + " " + owner);

        TransitionTo(guardBreakState);
    }
    #endregion

}
