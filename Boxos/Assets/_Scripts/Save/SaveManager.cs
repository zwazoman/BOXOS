using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

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

    List<PlayerProfile> profiles = new();

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
        print(Application.persistentDataPath);
        profiles = ReadProfiles();
    }

    public void SaveProfile(PlayerProfile profile)
    {
        print("save");
        profiles.Add(profile);
        SaveProfiles();
    }

    public void RemoveProfile(PlayerProfile profile)
    {
        if(profiles.Contains(profile))
            profiles.Remove(profile);
        SaveProfiles();
    }
    void SaveProfiles()
    {
        SavableList<PlayerProfile> savable = new();
        savable.list = profiles;

        File.WriteAllText(profileSavePath, JsonUtility.ToJson(savable));
    }

    public List<PlayerProfile> ReadProfiles()
    {
        print("read");
        if (File.Exists(profileSavePath))
        {
            profiles = JsonUtility.FromJson<SavableList<PlayerProfile>>(File.ReadAllText(profileSavePath)).list;
        }

        return profiles;
    }

    public void ClearProfiles()
    {
        print("clear");
        File.Delete(profileSavePath);
    }
}

[Serializable]
public class SavableList<T>
{
    public List<T> list = new();
}
