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

    [SerializeField] PlayerInput _playerInput;

    SyncVar<int> _stamina;
    SyncVar<int> _health;

    [SerializeField] public Arm leftArm;
    [SerializeField] public Arm rightArm;

    [HideInInspector] public Vector2 leftArmInputDelta = Vector2.zero;
    [HideInInspector] public Vector2 rightArmInputDelta = Vector2.zero;

    bool _regenStamina;

    private void Awake()
    {
        if (_camera == null)
            _camera = GetComponentInChildren<CinemachineCamera>();
        if (_playerInput == null)
            TryGetComponent(out _playerInput);
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();

        //_stamina.value = PlayerStats.MaxStamina;
        //_health.value = PlayerStats.MaxHealth;

        if (isOwner)
        {
            _body.SetActive(false);
            _camera.enabled = true;
        }
        else
        {
            _body.SetActive(true);
            _camera.enabled = false;
            GameManager.Instance.opponent = this;
            _playerInput.enabled = false;
        }
    }

    private void Update()
    {
        if (!isOwner)
            return;
    }

    public void OnLeftArmControl(InputAction.CallbackContext ctx)
    {
        leftArmInputDelta = new Vector2(ctx.ReadValue<Vector2>().x * -1, ctx.ReadValue<Vector2>().y);
    }

    public void OnRightArmControl(InputAction.CallbackContext ctx)
    {
        rightArmInputDelta = ctx.ReadValue<Vector2>();
    }

    public Vector2 GetStickVector(ArmSide side)
    {
        if(side == ArmSide.Left)
            return leftArmInputDelta;
        else
            return rightArmInputDelta;
    }


    public void OnKickInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            OnKick?.Invoke();
            print("Kick");
        }
    }





    public int TakeDamage(int amount)
    {
        OnTakeDamage?.Invoke();

        _health.value -= amount;
        Mathf.Clamp(_health, 0, PlayerStats.MaxHealth);

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

    public void Exhaust()
    {
        OnExhaust?.Invoke();
    }

    public void Die()
    {
        OnDie?.Invoke();
    }
}
