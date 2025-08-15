using UnityEngine;
using System.Collections.Generic;

public class ProfileSelection : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] GameObject _emptyProfilePrefab;
    [SerializeField] GameObject _newProfilePrefab;

    [Header("References")]
    [SerializeField] Transform _profilesPanel;

    private void OnEnable()
    {
        GenerateProfiles(SaveManager.Instance.ReadProfiles());
    }

    public void GenerateProfiles(List<PlayerProfile> profiles)
    {
        foreach (PlayerProfile playerProfile in profiles)
        {
            //mieux avec une pool
            Profile profile; 
            Instantiate(_emptyProfilePrefab, _profilesPanel).TryGetComponent(out profile);
            profile.FillProfile(playerProfile);
        }

        Instantiate(_newProfilePrefab, _profilesPanel);
    }
}
