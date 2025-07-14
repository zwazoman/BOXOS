using PurrNet;
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

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        
    }

    public void StartGame()
    {
        // fait spawn les deux joueurs  chacun de leur coté. chacun se voit et voit l'autre différemment (vue fps et vue tps)
    }

}
