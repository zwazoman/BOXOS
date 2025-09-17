using UnityEngine;

public class SelectedProfile : MonoBehaviour
{
    Profile _profile;

    private void Awake()
    {
        TryGetComponent(out _profile);
    }

    private void Start()
    {
        GameData.Instance.OnProfileChanged += _profile.FillProfile;
    }
}
