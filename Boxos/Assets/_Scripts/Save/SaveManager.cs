using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    private static SaveManager instance;

    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Save Manager");
                instance = go.AddComponent<SaveManager>();
            }
            return instance;
        }
    }
    #endregion

    List<PlayerProfile> profiles;

    string profileSavePath;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        profileSavePath = Application.persistentDataPath + "/Profiles";
        profiles = ReadProfiles();
    }

    public void SaveProfile(PlayerProfile profile)
    {
        profiles.Add(profile);
        SaveProfiles();
    }

    public void RemoveProfile(PlayerProfile profile)
    {
        if(profiles.Contains(profile))
            profiles.Remove(profile);
        SaveProfiles();
    }

    public List<PlayerProfile> ReadProfiles()
    {
        List<PlayerProfile> profiles = new();

        if(File.Exists(profileSavePath))
            profiles = JsonUtility.FromJson<List<PlayerProfile>>(profileSavePath);

        return profiles;
    }

    public void ClearProfiles()
    {
        File.Delete(profileSavePath);
    }

    void SaveProfiles()
    {
        string json = JsonUtility.ToJson(profiles);

        File.WriteAllText(profileSavePath, json);
    }
}
