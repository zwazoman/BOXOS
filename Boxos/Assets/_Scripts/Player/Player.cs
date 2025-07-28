using PurrNet;
using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : NetworkIdentity
{
    #region Events

    public event Action OnTakeDamage;
    public event Action OnLoseStamina;
    public event Action OnStaminaRegen;
    public event Action OnOverheat;
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

    public SyncVar<int> heat = new(initialValue: PlayerStats.MaxHeat, ownerAuth: true);
    public SyncVar<int> health = new(initialValue: PlayerStats.MaxHealth, ownerAuth: true);

    [SerializeField] public Arm leftArm;
    [SerializeField] public Arm rightArm;

    [HideInInspector] public Vector2 leftArmInputDelta = Vector2.zero;
    [HideInInspector] public Vector2 rightArmInputDelta = Vector2.zero;

    bool _cooling;
    float _coolTimer;

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
            print(localPlayerForced);
            _body.SetActive(false);
            _camera.enabled = true;
            StartCoroutine(CoolHeat());
            _cooling = true;
        }
        else
        {
            _body.SetActive(true);
            _camera.enabled = false;
            GameManager.Instance.opponent = this;
            GameManager.Instance.opponentId = owner.Value;
            
            _playerInput.enabled = false;
        }

        GameManager.Instance.PlayerSpawned();
    }

    private void Update()
    {
        if (!isOwner)
            return;

        if (!_cooling)
        {
            _coolTimer += Time.deltaTime;
            if(_coolTimer >= PlayerStats.HeatTimeToCool)
            {
                _cooling = true;
                _coolTimer = 0;
                StartCoroutine(CoolHeat());
            }
        }
    }

    public void OnLeftArmControl(InputAction.CallbackContext ctx)
    {
        leftArmInputDelta = new Vector2(ctx.ReadValue<Vector2>().x * -1, ctx.ReadValue<Vector2>().y);
    }

    public void OnRightArmControl(InputAction.CallbackContext ctx)
    {
        rightArmInputDelta = ctx.ReadValue<Vector2>();
    }

    public void OnCancelLeft(InputAction.CallbackContext ctx)
    {
        leftArm.Cancel();
    }

    public void OnCancelRight(InputAction.CallbackContext ctx)
    {
        rightArm.Cancel();
    }

    public Vector2 GetStickVector(ArmSide side)
    {
        if (side == ArmSide.Left)
            return leftArmInputDelta;
        return rightArmInputDelta;
    }

    public Arm GetOpposedArm(ArmSide side)
    {
        if (side == ArmSide.Left)
            return rightArm;
        return leftArm;
    }

    public void UpdateHealth(int amount)
    {
        if (!isOwner)
            return;

        OnTakeDamage?.Invoke();

        health.value = Mathf.Clamp(health + amount, 0, PlayerStats.MaxHealth);

        if (health == 0)
            Die();
    }

    public void UpdateHeat(int amount)
    {
        if (!isOwner)
            return;

        OnLoseStamina?.Invoke();

        if (heat.value < heat.value + amount)
        {
            _cooling = false;
        }

        heat.value = Mathf.Clamp(heat + amount, 0, PlayerStats.MaxHeat);

        if (heat == PlayerStats.MaxHeat)
        {
            print("OVERHEAT");
            OnOverheat?.Invoke();
        }
    }

    IEnumerator CoolHeat()
    {
        while (_cooling)
        {
            UpdateHeat(-PlayerStats.StaminaRegenPerTick);
            yield return new WaitForSeconds(PlayerStats.StaminaRegenDurationOffset);
        }
    }

    public void Die()
    {
        OnDie?.Invoke();
    }
}
