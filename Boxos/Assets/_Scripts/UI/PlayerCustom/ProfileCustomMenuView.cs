using PurrLobby;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ProfileCustomMenuView : MenuView
{
    [HideInInspector] public PlayerProfile playerProfile;
    [HideInInspector] public PlayerProfile? oldPlayerProfile;

    [SerializeField] public ProsthesisData emptyProsthesis;

    [SerializeField] ProsthesisCustomSlot _rightSlot;
    [SerializeField] ProsthesisCustomSlot _leftSlot;

    [SerializeField] NameCustomSlot _nameSlot;

    public void LoadProfile(PlayerProfile profile)
    {
        playerProfile = profile;
        oldPlayerProfile = profile;

        _rightSlot.SetData(profile.rightArmData);
        _leftSlot.SetData(profile.leftArmData);

        _nameSlot.SetData(profile.profileName);
    }


    public void SaveProfile()
    {
        SaveManager.Instance.SaveProfile(playerProfile, oldPlayerProfile);
    }
}
