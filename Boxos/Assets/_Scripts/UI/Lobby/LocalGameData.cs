using UnityEngine;

public class LocalGameData : MonoBehaviour
{
    public PlayerProfile Profile1;
    public PlayerProfile Profile2;

    public void SetProfile(PlayerProfile profile,int index)
    {
        switch (index = 1)
        {
            case 1:
                Profile1 = profile;
                break;
            case 2:
                Profile2 = profile;
                break;
        }
    }
}
