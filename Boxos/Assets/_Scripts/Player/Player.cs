using PurrNet;
using System;
using Unity.Cinemachine;
using UnityEngine;

public class Player : NetworkIdentity
{
    #region Events

    public event Action OnTakeDamage;
    public event Action OnLoseStamina;
    public event Action OnStaminaRegen;
    public event Action OnExhaust;
    public event Action OnDie;

    #endregion

    [SerializeField] GameObject _body;
    [SerializeField] CinemachineCamera _camera;

    //SyncVar<int> _stamina;
    //SyncVar<int> _health;

    int _stamina;
    int _health;

    bool _regenStamina;

    private void Awake()
    {
        if(_camera == null)
            _camera = GetComponentInChildren<CinemachineCamera>();

        _stamina = PlayerStats.MaxStamina;
        _health = PlayerStats.MaxHealth;
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();

        if (isOwner)
        {
            _body.SetActive(false);
            _camera.enabled = true;
        }
        else
        {
            _body.SetActive(true);
            _camera.enabled = false;
        }
    }

    public int TakeDamage(int amount)
    {
        OnTakeDamage?.Invoke();

        _health -= amount;
        Mathf.Clamp(_health,0,PlayerStats.MaxHealth);

        if (_health == 0)
            Die();

        return _health;
    }

    public int LoseStamina(int amount)
    {
        OnLoseStamina?.Invoke();

        _stamina -= amount;
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
