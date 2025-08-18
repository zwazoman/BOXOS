using UnityEngine;
using System.Collections.Generic;

public class ProfileSelection : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] GameObject _emptyProfilePrefab;
    [SerializeField] GameObject _newProfilePrefab;
    [SerializeField] int _maxProfilesAmount = 4;

    [Header("References")]
    [SerializeField] Transform _profilesPanel;

    List<Profile> _uiProfiles = new();

    private void OnEnable()
    {
        GenerateProfiles(SaveManager.Instance.ReadProfiles());
    }

    private void OnDisable()
    {
        DeleteProfiles();
    }

    void GenerateProfiles(List<PlayerProfile> profiles)
    {
        foreach (PlayerProfile playerProfile in profiles)
        {
            //mieux avec une pool
            Profile profile; 
            Instantiate(_emptyProfilePrefab, _profilesPanel).TryGetComponent(out profile);
            profile.FillProfile(playerProfile);
            _uiProfiles.Add(profile);
        }

        if(profiles.Count < _maxProfilesAmount)
            _uiProfiles.Add(Instantiate(_newProfilePrefab, _profilesPanel).GetComponent<Profile>());
    }

    void DeleteProfiles()
    {
        foreach(Profile profile in _uiProfiles)
        {
            Destroy(profile.gameObject);
        }

        _uiProfiles.Clear();
    }
}
