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

    public PlayerProfile playerData;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }
}

[Serializable] // A RETIRER
public struct PlayerProfile
{
    public ProsthesisData rightArmData;
    public ProsthesisData leftArmData;
}
