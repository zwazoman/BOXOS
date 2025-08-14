using Cysharp.Threading.Tasks;
using System;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class MenuView : MonoBehaviour
{
    public event Action OnEnable;
    public event Action OnDisable;

    [HideInInspector] public CinemachineCamera cam;

    [SerializeField] CinemachineCamera viewCam;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        TryGetComponent(out _canvasGroup);
    }

    public virtual async void Enable()
    {
        viewCam.enabled = true;
        OnEnable?.Invoke();
        await _canvasGroup.Show();
    }

    public virtual async void Disable()
    {
        await _canvasGroup.Hide();
        OnDisable?.Invoke();
        viewCam.enabled = false;
    }

}
