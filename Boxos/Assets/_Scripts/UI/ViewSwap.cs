using Unity.Cinemachine;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class ViewSwap : MonoBehaviour
{
    #region Singleton
    private static ViewSwap instance;

    public static ViewSwap Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("View Swap");
                instance = go.AddComponent<ViewSwap>();
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] CinemachineCamera cam;

    [Header("Views")]
    [SerializeField] public MenuView mainMenu;
    [SerializeField] public MenuView playerCustomisation;
    [SerializeField] public MenuView Lobby;

    MenuView currentView;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        SwapTo(mainMenu);
    }

    public void SwapTo(MenuView view)
    {
        if(currentView != null)
            currentView.Disable();

        currentView = view;
        if(currentView.cam == null)
            currentView.cam = cam;

        view.Enable();
    }

    public void SwapTo(string viewName)
    {
        switch (viewName)
        {
            case "MainMenu":
                SwapTo(mainMenu);
                break;
            case "PlayerCustom":
                SwapTo(playerCustomisation);
                break;
            case "Lobby":
                SwapTo(Lobby);
                break;
        }
    }

}
