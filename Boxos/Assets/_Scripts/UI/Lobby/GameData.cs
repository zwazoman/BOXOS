using NUnit.Framework;
using PurrNet;
using UnityEngine;
using System.Collections.Generic;
using System;

public class GameData : MonoBehaviour
{
    #region Singleton
    private static GameData instance;

    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Game Data");
                instance = go.AddComponent<GameData>();
            }
            return instance;
        }
    }
    #endregion

    public event Action OnProfileChanged;

    public PlayerProfile playerProfile;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }

    public void ChangePlayerProfile(PlayerProfile profile)
    {
        playerProfile = profile;
        OnProfileChanged?.Invoke();
    }

}

[Serializable]
public struct PlayerProfile
{
    public string profileName;

    public ProsthesisData rightArmData;
    public ProsthesisData leftArmData;
}
