using PurrNet;
using System;
using UnityEngine;

public class Arm : MonoBehaviour
{
    #region Events
    public event Action OnPrepAttack;

    public event Action OnLightAttack;
    public event Action OnHeavyAttack;

    public event Action OnBlockStart;
    public event Action OnBlockEnd;

    public event Action OnParry;

    public event Action<float> OnStagger;

    public event Action OnExhaust;

    
    public event Action OnAnimationEnd;

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

    private void Start()
    {
        AnimatorStateInfo currenStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currenStateInfo.normalizedTime >= 1)
            OnAnimationEnd?.Invoke();
    }
}
