using PurrNet;
using TMPro;
using UnityEngine;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] Player _player;

    [Header("Owner Section")]

    [SerializeField] Canvas _ownerCanvas;
    [SerializeField] TMP_Text _ownerHpText;
    [SerializeField] TMP_Text _ownerStaminaText;

    [Header("Opponent Section")]

    [SerializeField] Canvas _opponentCanvas;
    [SerializeField] TMP_Text _opponentHpText;
    [SerializeField] TMP_Text _opponentStaminaText;

    private void Awake()
    {
        if(_player == null)
            _player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        _player.health.onChanged += UpdateHealth;
        _player.stamina.onChanged += UpdateStamina;
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();

        if (isOwner)
        {
            _opponentCanvas.enabled = false;
            _ownerHpText.text = PlayerStats.MaxHealth.ToString();
            _ownerStaminaText.text = PlayerStats.MaxStamina.ToString();
        }
        else
        {
            _ownerCanvas.enabled = false;
            _opponentHpText.text = PlayerStats.MaxHealth.ToString();
            _opponentStaminaText.text = PlayerStats.MaxStamina.ToString();
        }

    }

    void UpdateHealth(int newAmount)
    {
        if (isOwner)
            _ownerHpText.text = newAmount.ToString();
        else
            _opponentHpText.text = newAmount.ToString();
    }

    void UpdateStamina(int newAmount)
    {
        if(isOwner)
            _ownerStaminaText.text = newAmount.ToString();
        else
            _opponentStaminaText.text = newAmount.ToString();
    }
}
