using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    #region Singleton
    private static PlayerInventory instance;

    public static PlayerInventory Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Player Inventory");
                instance = go.AddComponent<PlayerInventory>();
            }
            return instance;
        }
    }
    #endregion

    public ProsthesisData rightProsthesis;
    public ProsthesisData leftProsthesis;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }
}
