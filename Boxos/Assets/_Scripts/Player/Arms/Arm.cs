using PurrNet;
using System;
using UnityEngine;

public class Arm : NetworkIdentity
{
    #region Events
    public event Action OnParry;
    public event Action OnParried;

    public event Action OnBlock;
    public event Action OnBlocked;

    public event Action OnHit;
    public event Action<Arm, int> OnReceiveHit;

    public event Action OnExhaust;

    public event Action OnAnimationCycle;

    #endregion

    [SerializeField] public Player player;
    [SerializeField] public ArmSide side;
    [SerializeField] public NetworkAnimator animator;
    [SerializeField] public ArmStateMachine stateMachine;



    private void Awake()
    {
        if (animator == null)
            TryGetComponent(out animator);
        if (player == null)
            GetComponentInParent<Player>();
        if(stateMachine == null)
            TryGetComponent(out stateMachine);
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();
    }

    public void Hit(int hitid)
    {
        if (!isOwner)
            return;

        print("han");
        if (GameManager.Instance.opponent == null)
        {
            print("no opponent to hit");
            return;
        }

        Arm targetArm = GameManager.Instance.opponent.GetOpposedArm(side);

        targetArm.ReceiveHit(GameManager.Instance.opponentId, this, hitid);

        //handle baisse de stamina
    }

    public void ReceiveHit(PlayerID id, Arm attackingArm,int attackID)
    {
        print("allo " + attackingArm);
        OnReceiveHit?.Invoke(attackingArm, attackID);
    }

    public void Blocked() { OnBlocked?.Invoke(); }
    public void Parried() { OnParried?.Invoke(); }

    private void Update()
    {
        if (!isOwner)
            return;

        AnimatorStateInfo currenStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currenStateInfo.normalizedTime >= 1)
            OnAnimationCycle?.Invoke();
    }
}
