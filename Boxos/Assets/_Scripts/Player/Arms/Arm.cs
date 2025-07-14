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

    #endregion

    [SerializeField] NetworkAnimator _animator;
    [SerializeField] Player _player;

    private void Awake()
    {
        if (_animator == null)
            TryGetComponent(out _animator);
        if (_player == null)
            GetComponentInParent<Player>();
    }

    private void Start()
    {
        
    }



}
