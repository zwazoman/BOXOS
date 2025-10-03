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
    public event Action<Arm,HitData> OnReceiveHit;
    public event Action<Arm, HitData> OnReceiveTrueHit;

    public event Action OnGuardBreak;
    public event Action<Arm> OnReceiveGuardBreak;
    public event Action OnSuccessfullGuardBreak;

    public event Action OnCancel;

    public event Action<bool> OnParryWindow;
    public event Action<bool> OnCancelWindow;

    public event Action OnExhaust;

    public event Action OnAnimationEnd;

    #endregion

    [SerializeField] public OnlinePlayer player;
    [SerializeField] public ArmSide side;
    [SerializeField] public NetworkAnimator animator;
    [SerializeField] public ArmStateMachine stateMachine;
    [SerializeField] public ArmInputsHandler inputs;
    [SerializeField] public ProstheticsHanlder prosthetics;

    bool _checkAnimationCycle = false;

    private void Awake()
    {
        if (animator == null)
            TryGetComponent(out animator);
        if (player == null)
            GetComponentInParent<OnlinePlayer>();
        if(stateMachine == null)
            TryGetComponent(out stateMachine);
        if(inputs == null)
            TryGetComponent(out inputs);
        if(prosthetics == null)
            TryGetComponent(out prosthetics);
    }

    private void Start()
    {
        player.OnOverheat += () => OnExhaust?.Invoke();
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();
    }

    public void GuardBreak()
    {
        if (!isOwner)
            return;

        Arm targetArm = FightManager.Instance.opponent.GetOpposedArmBySide(side);

        targetArm.ReceiveGuardBreak(FightManager.Instance.opponentId, this);

    }

    public void Cancel()
    {
        if (!isOwner)
            return;
        OnCancel?.Invoke();
    }


    [TargetRpc]
    public void ReceiveHit(PlayerID id, Arm attackingArm, HitData hitData)
    {
        print("receivehit");
        OnReceiveHit?.Invoke(attackingArm, hitData); 
    }

    [TargetRpc]
    public void ReceiveTrueHit(PlayerID id, Arm attackingArm,HitData hitData)
    {
        OnReceiveTrueHit?.Invoke(attackingArm, hitData);
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

    public void Hit() { if(isOwner) OnHit?.Invoke(); }

    public void ParryWindow(bool state){ OnParryWindow?.Invoke(state); }

    public void CancelWindow(bool state) { OnCancelWindow?.Invoke(state); }

    public void AnimationEnd() { OnAnimationEnd?.Invoke(); }
}
