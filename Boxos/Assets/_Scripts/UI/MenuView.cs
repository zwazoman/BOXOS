using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class MenuView : MonoBehaviour
{
    [HideInInspector] public CinemachineCamera cam;

    [SerializeField] CinemachineCamera viewCam;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        TryGetComponent(out _canvasGroup);
    }

    public async void Enable()
    {
        viewCam.enabled = true;
        await _canvasGroup.Show();
    }

    public async void Disable()
    {
        await _canvasGroup.Hide();
        viewCam.enabled = false;
    }

}
