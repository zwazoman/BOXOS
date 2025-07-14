using Mono.Cecil.Cil;
using PurrNet;
using UnityEngine;

public class ArmStateMachine : MonoBehaviour
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

    void Start()
    {
        TransitionTo(neutralState);
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
        currentState._arm = _arm;

        currentState.OnEnter();
    }

    void Update()
    {
        currentState.Update();
    }

}
