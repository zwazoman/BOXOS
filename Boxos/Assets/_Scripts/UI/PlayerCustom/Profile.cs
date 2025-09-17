using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    [HideInInspector] public ProfilesHandler _profileSelection;
    [HideInInspector] public PlayerProfile playerProfile;

    [HideInInspector] public int index;

    [SerializeField] TMP_Text _nameText;

    Button selectButton;

    private void Start()
    {
        _profileSelection = GetComponentInParent<ProfilesHandler>();
    }

    public void FillProfile(PlayerProfile profile)
    {
        playerProfile = profile;

        _nameText.text = profile.profileName;
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
        playerProfile = new();
        playerProfile.leftArmData = ViewSwap.Instance.profileCustomisation.emptyProsthesis;
        playerProfile.rightArmData = ViewSwap.Instance.profileCustomisation.emptyProsthesis;
        playerProfile.profileName = "NewProfile " + SaveManager.Instance.profiles.Count;

        //SaveManager.Instance.AddProfile(playerProfile);
        EditProfile();
    }

    public void DeleteProfile()
    {
        SaveManager.Instance.RemoveProfile(playerProfile);
        Destroy(gameObject);
    }
}
