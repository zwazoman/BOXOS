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
    [SerializeField] public MainMenuMenuView mainMenu;
    [SerializeField] public ProfileCustomMenuView profileCustomisation;
    [SerializeField] public LobbyMenuView lobby;

    MenuView _previousView;
    MenuView _currentView;

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
        if(_currentView != null)
            _currentView.Disable();

        _previousView = _currentView;

        _currentView = view;
        if(_currentView.cam == null)
            _currentView.cam = cam;

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
                SwapTo(profileCustomisation);
                break;
            case "Lobby":
                SwapTo(lobby);
                break;
        }
    }

    public void SwapToPrevious()
    {
        if(_previousView != null)
            SwapTo(_previousView);
    }

}
