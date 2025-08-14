using UnityEngine;
using System.Collections.Generic;

public class ProfileSelection : MonoBehaviour
{
    public List<PlayerProfile> profiles = new();

    [Header("Parameters")]
    [SerializeField] GameObject _emptyProfilePrefab;
    [SerializeField] GameObject _newProfilePrefab;

    [Header("References")]
    [SerializeField] Transform _profilesPanel;

    private void Start()
    {
        //récupérer les données sauvegardées et les stocker dans profiles
    }

    private void OnEnable()
    {
        GenerateProfiles();
    }

    public void GenerateProfiles()
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
