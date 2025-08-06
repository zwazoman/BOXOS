using NUnit.Framework;
using PurrNet;
using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using System;

public class ArmStateMachine : NetworkIdentity
{
    [SerializeField] Arm _arm;

    ArmState currentState;

    [SerializeField] public SerializedDictionary<ActionType, AttackTruc> attacks = new();
    [SerializeField] public SerializedDictionary<ActionType,UtilitaryTruc> utilitaries = new();

    public Dictionary<ActionType, ActionState> actionStatesByTypes = new();

    #region States
    public NeutralState neutralState = new();
    public StaggerState staggerState = new();
    public OverHeatState overHeatState = new();
    public AttackPrepState attackPrepState = new();
    public AttackState attackState = new();
    public UtilitaryPrepState utilitaryPrepState = new();

    //Actions

    //attacks
    public LightAttackState lightAttackState = new();
    public HeavyAttackState heavyAttackState = new();
    public OtherSideAttackState otherSideAttackState = new();
    public BlockBreakAttackState blockBreakAttackState = new();
    public ChargedAttackState chargedAttackState = new();
    public DefenseBreakAttack defenseBreakAttack = new();


    //Utilitaries
    public BlockState blockState = new();
    public GuardBreakState guardBreakState = new();
    public RecoveryState recoveryState = new();
    public CounterState counterState = new();


    void FillActionStates()
    {
        actionStatesByTypes.Add(ActionType.LightAttack, lightAttackState);
        actionStatesByTypes.Add(ActionType.HeavyAttack, heavyAttackState);

        actionStatesByTypes.Add(ActionType.OtherSideAttack, otherSideAttackState);
        actionStatesByTypes.Add(ActionType.BlockBreakAttack, blockBreakAttackState);
        actionStatesByTypes.Add(ActionType.ChargedAttack, chargedAttackState);
        actionStatesByTypes.Add(ActionType.DefenseBreakAttack, defenseBreakAttack);


        actionStatesByTypes.Add(ActionType.Block, blockState);
        actionStatesByTypes.Add(ActionType.GuardBreak, guardBreakState);

        actionStatesByTypes.Add(ActionType.Recovery, recoveryState);
        actionStatesByTypes.Add(ActionType.Counter, counterState);

    }
    #endregion

    private void Awake()
    {
        if (_arm == null)
            TryGetComponent(out _arm);

        FillActionStates();
    }
    protected override void OnSpawned()
    {
        base.OnSpawned();

        if (!isOwner)
            return;

        if (FightManager.Instance.opponentId == PlayerID.Server)
            FightManager.Instance.OnPlayerSpawned += Neutral;
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

    public void Neutral()
    {
        if(isOwner) print("Neutral" + " " + owner);

        TransitionTo(neutralState);
    }

    public void Stagger(float duration = 0)
    {
        if (isOwner) print("Stagger" + " " + owner);

        staggerState.stateDuration = duration;
        TransitionTo(staggerState);
    }

    public void OverHeat()
    {
        if (isOwner) print("Overheat" + " " + owner);

        TransitionTo(overHeatState);
    }

    public void AttackPrep()
    {
        if (isOwner) print("AttackPrep" + " " + owner);

        TransitionTo(attackPrepState);
    }

    public void UtilitaryPrep()
    {
        if (isOwner) print("Utilitary Prep" + " " + owner);

        TransitionTo(utilitaryPrepState);
    }
    #endregion

}

[Serializable]
public struct AttackTruc
{
    public ActionType type;
    public AttackData data;

    public AttackTruc(ActionType type, AttackData data)
    {
        this.type = type;
        this.data = data;
    }
}

[Serializable]
public struct UtilitaryTruc
{
    public ActionType type;
    public UtilitaryData data;

    public UtilitaryTruc(ActionType type, UtilitaryData data)
    {
        this.type = type;
        this.data = data;
    }
}
