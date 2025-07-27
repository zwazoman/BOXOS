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

    public SyncVar<int> stamina = new(initialValue: PlayerStats.MaxStamina, ownerAuth: true);
    public SyncVar<int> health = new(initialValue: PlayerStats.MaxHealth, ownerAuth: true);

    [SerializeField] public Arm leftArm;
    [SerializeField] public Arm rightArm;

    [HideInInspector] public Vector2 leftArmInputDelta = Vector2.zero;
    [HideInInspector] public Vector2 rightArmInputDelta = Vector2.zero;

    bool staminaRegen;
    float staminaTimer;

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
            StartCoroutine(RegenStamina());
            staminaRegen = true;
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

        if (!staminaRegen)
        {
            staminaTimer += Time.deltaTime;
            if(staminaTimer >= PlayerStats.StaminaStaggerDuration)
            {
                staminaRegen = true;
                staminaTimer = 0;
                StartCoroutine(RegenStamina());
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

    public void UpdateStamina(int amount)
    {
        if (!isOwner)
            return;

        OnLoseStamina?.Invoke();

        if (stamina.value > stamina.value + amount)
        {
            staminaRegen = false;
        }

        stamina.value = Mathf.Clamp(stamina + amount, 0, PlayerStats.MaxStamina);

        if (stamina == 0)
        {
            print("EXHAUST");
            OnExhaust?.Invoke();
        }
    }

    IEnumerator RegenStamina()
    {
        while (staminaRegen)
        {
            UpdateStamina(PlayerStats.StaminaRegenPerTick);
            yield return new WaitForSeconds(PlayerStats.StaminaRegenDurationOffset);
        }
    }

    public void Die()
    {
        OnDie?.Invoke();
    }
}
