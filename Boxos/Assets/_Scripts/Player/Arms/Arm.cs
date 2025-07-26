using PurrNet;
using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Arm : NetworkIdentity
{
    #region Events
    public event Action OnParry;
    public event Action OnParried;

    public event Action OnBlock;
    public event Action OnBlocked;

    public event Action OnHit;
    public event Action<Arm, int> OnReceiveHit;

    public event Action OnGuardBreak;
    public event Action<Arm> OnReceiveGuardBreak;
    public event Action OnSuccessfullGuardBreak;

    public event Action<bool> OnParryWindow;

    public event Action OnExhaust;

    public event Action OnAnimationCycle;

    #endregion

    [SerializeField] public Player player;
    [SerializeField] public ArmSide side;
    [SerializeField] public Animator animator;
    [SerializeField] public ArmStateMachine stateMachine;

    bool _checkAnimationCycle = false;

    private void Awake()
    {
        if (animator == null)
            TryGetComponent(out animator);
        if (player == null)
            GetComponentInParent<Player>();
        if(stateMachine == null)
            TryGetComponent(out stateMachine);
    }

    private void Start()
    {
        player.OnExhaust += () => OnExhaust?.Invoke();
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();
    }

    public void Hit(int hitid)
    {
        //gérer juice

        if (!isOwner)
            return;

        print("HIT");

        Arm targetArm = GameManager.Instance.opponent.GetOpposedArm(side);

        targetArm.ReceiveHit(GameManager.Instance.opponentId, this, hitid);

        //handle baisse de stamina
    }

    public void GuardBreak()
    {
        if (!isOwner)
            return;

        Arm targetArm = GameManager.Instance.opponent.GetOpposedArm(side);

        targetArm.ReceiveGuardBreak(GameManager.Instance.opponentId, this);

    }

    [TargetRpc]
    public void ReceiveHit(PlayerID id, Arm attackingArm,int attackID)
    { 
        OnReceiveHit?.Invoke(attackingArm, attackID); 
    }

    [TargetRpc]
    public void ReceiveGuardBreak(PlayerID id, Arm guardBreakingArm)
    {
        OnReceiveGuardBreak?.Invoke(guardBreakingArm);
    }

    [TargetRpc]
    public void SuccessfullGuardbreak(PlayerID id) { OnSuccessfullGuardBreak?.Invoke(); }

    [TargetRpc]
    public void Blocked(PlayerID id) { OnBlocked?.Invoke(); }

    [TargetRpc]
    public void Parried(PlayerID id) { OnParried?.Invoke(); }

    public void ParryWindow(bool state){    OnParryWindow?.Invoke(state);   }

    public void CheckAnimationCycle()
    {
        _checkAnimationCycle = true;
    }

    private void Update()
    {
        if (!isOwner || !_checkAnimationCycle)
            return;

        AnimatorStateInfo currenStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currenStateInfo.normalizedTime >= 1)
        {
            OnAnimationCycle?.Invoke();
            _checkAnimationCycle = false;
        }
    }
}
