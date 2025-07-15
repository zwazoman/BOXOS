using PurrNet;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : NetworkIdentity
{
    #region Events

    public event Action OnTakeDamage;
    public event Action OnLoseStamina;
    public event Action OnStaminaRegen;
    public event Action OnExhaust;
    public event Action OnDie;

    public event Action OnKick;

    public event Action<ArmSide> OnPrepareAttackStart;
    public event Action<ArmSide> OnPrepareAttackEnd;
    public event Action<ArmSide> OnLightAttack;
    public event Action<ArmSide> OnHeavyAttack;

    public event Action<ArmSide> OnBlockStart;
    public event Action<ArmSide> OnBlockEnd;
    public event Action<ArmSide> OnParry;

    #endregion

    [SerializeField] GameObject _body;
    [SerializeField] CinemachineCamera _camera;

    [SerializeField] BoxosInputActions _inputs;

    SyncVar<int> _stamina;
    SyncVar<int> _health;

    InputAction _leftStickMove;
    InputAction _rightStickMove;

    bool _regenStamina;

    private void Awake()
    {
        if(_camera == null)
            _camera = GetComponentInChildren<CinemachineCamera>();
        if(_inputs == null)
            TryGetComponent(out _inputs);
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();

        _stamina.value = PlayerStats.MaxStamina;
        _health.value = PlayerStats.MaxHealth;

        if (isOwner)
        {
            _body.SetActive(false);
            _camera.enabled = true;

            _leftStickMove = _inputs.Boxe.LeftArm;
            _leftStickMove.Enable();

            _rightStickMove = _inputs.Boxe.RightArm;
            _rightStickMove.Enable();

            _inputs.Boxe.Kick.performed += KickInput;
            _inputs.Boxe.ParryRight.performed += ParryRight;
            _inputs.Boxe.ParryLeft.performed += ParryLeft;
        }
        else
        {
            _body.SetActive(true);
            _camera.enabled = false;
            GameManager.Instance.opponent = this;
        }
    }

    private void Update()
    {
        if (!isOwner)
            return;

        //là faut check pour les inputs spéciaux des sticks genre arc de cercle etc Atan2

    }

    void KickInput(InputAction.CallbackContext ctx)
    {
        OnKick?.Invoke();
    }

    void ParryLeft(InputAction.CallbackContext ctx)
    {
        OnParry?.Invoke(ArmSide.Left);
    }

    void ParryRight(InputAction.CallbackContext ctx)
    {
        OnParry?.Invoke(ArmSide.Right);
    }

    public int TakeDamage(int amount)
    {
        OnTakeDamage?.Invoke();

        _health.value -= amount;
        Mathf.Clamp(_health,0,PlayerStats.MaxHealth);

        if (_health == 0)
            Die();

        return _health;
    }

    public int LoseStamina(int amount)
    {
        OnLoseStamina?.Invoke();

        _stamina.value -= amount;
        Mathf.Clamp(_stamina, 0, PlayerStats.MaxStamina);

        if (_stamina == 0)
            Exhaust();

        return _stamina;
    }

    public  void Exhaust()
    {
        OnExhaust?.Invoke();
    }

    public void Die()
    {
        OnDie?.Invoke();
    }
}
