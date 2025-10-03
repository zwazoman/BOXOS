using PurrNet;
using System;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    #region Singleton
    private static FightManager instance;

    public static FightManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<FightManager>();
            }
            return instance;
        }
    }
    #endregion

    public event Action OnPlayerSpawned;

    [SerializeField] public NetworkManager networkManager;

    [HideInInspector] public OnlinePlayer opponent;
    [HideInInspector] public PlayerID opponentId;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    public void PlayerSpawned()
    {
        OnPlayerSpawned?.Invoke();
    }

    private void Update()
    {
        //print(opponentId);
    }

}
