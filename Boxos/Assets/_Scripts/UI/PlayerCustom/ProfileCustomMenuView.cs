using PurrLobby;
using UnityEngine;

public class ProfileCustomMenuView : MenuView
{
    [HideInInspector] public PlayerProfile playerProfile = new();

    [SerializeField] public ProsthesisData emptyProsthesis;

    [SerializeField] ProsthesisSlot rightSlot;
    [SerializeField] ProsthesisSlot leftSlot;

    public void LoadProfile(PlayerProfile profile)
    {
        rightSlot.SetData(profile.rightArmData);
        leftSlot.SetData(profile.leftArmData);
    }

    public void CreateProfile()
    {
        rightSlot.SetData(emptyProsthesis);
        leftSlot.SetData(emptyProsthesis);
    }
}
