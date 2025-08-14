using UnityEngine;

public class Profile : MonoBehaviour
{
    [HideInInspector] public ProfileSelection _profileSelection;
    [HideInInspector] public PlayerProfile playerProfile;

    private void Start()
    {
        _profileSelection = GetComponentInParent<ProfileSelection>();
    }

    public void FillProfile(PlayerProfile profile)
    {
        playerProfile = profile;

        //mettre a jour les visuels de con
    }

    //buttons
    public void EditProfile()
    {
        ViewSwap.Instance.profileCustomisation.LoadProfile(playerProfile);
        ViewSwap.Instance.SwapTo(ViewSwap.Instance.profileCustomisation);
        _profileSelection.gameObject.SetActive(false);
    }

    public void SelectProfile()
    {
        GameData.Instance.ChangePlayerProfile(playerProfile);
        _profileSelection.gameObject.SetActive(false);
    }

    public void CreateProfile()
    {
        ViewSwap.Instance.profileCustomisation.CreateProfile();
        ViewSwap.Instance.SwapTo(ViewSwap.Instance.profileCustomisation);
        _profileSelection.gameObject.SetActive(false);
    }
}
