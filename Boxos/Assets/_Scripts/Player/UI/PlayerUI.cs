using PurrNet;
using TMPro;
using UnityEngine;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] Player _player;

    [Header("Owner Section")]

    [SerializeField] Canvas _ownerCanvas;
    [SerializeField] TMP_Text _ownerHpText;
    [SerializeField] TMP_Text _ownerHeatText;

    [Header("Opponent Section")]

    [SerializeField] Canvas _opponentCanvas;
    [SerializeField] TMP_Text _opponentHpText;
    [SerializeField] TMP_Text _opponentHeatText;

    private void Awake()
    {
        if(_player == null)
            _player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        _player.health.onChanged += UpdateHealth;
        _player.heat.onChanged += UpdateHeat;
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();
        if (isOwner)
        {
            _opponentCanvas.enabled = false;
            _ownerHpText.text = _player.health.value.ToString();
            _ownerHeatText.text = _player.heat.value.ToString();
        }
        else
        {
            _ownerCanvas.enabled = false;
            _opponentHpText.text = _player.health.value.ToString();
            _opponentHeatText.text = _player.heat.value.ToString();
        }

    }

    void UpdateHealth(int newAmount)
    {
        if (isOwner)
            _ownerHpText.text = newAmount.ToString();
        else
            _opponentHpText.text = newAmount.ToString();
    }

    void UpdateHeat(int newAmount)
    {
        if(isOwner)
            _ownerHeatText.text = newAmount.ToString();
        else
            _opponentHeatText.text = newAmount.ToString();
    }
}
