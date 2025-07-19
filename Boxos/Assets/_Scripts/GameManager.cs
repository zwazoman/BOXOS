using PurrNet;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    public event Action OnPlayerSpawned;

    [SerializeField] public NetworkManager networkManager;

    [HideInInspector] public Player opponent;
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
