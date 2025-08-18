using UnityEngine;

public class ProfileCustomSlot : MonoBehaviour
{
    [SerializeField] protected ProfileCustomMenuView view;

    protected virtual void Start()
    {
        if (view == null)
            view = FindAnyObjectByType<ProfileCustomMenuView>();
    }
}
