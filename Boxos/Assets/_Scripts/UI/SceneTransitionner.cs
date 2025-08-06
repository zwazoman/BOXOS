using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionner : MonoBehaviour
{
    #region Singleton
    private static SceneTransitionner instance;

    public static SceneTransitionner Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Scene Transitionner");
                instance = go.AddComponent<SceneTransitionner>();
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] CanvasGroup _transitionCanvasGroup;
    [SerializeField] float _transitionDuration;

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += async (_,_) => await FadeOut();
        _transitionCanvasGroup.alpha = 0;
    }

    public async void ChangeScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
        await FadeIn();
    }

    public async void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        await FadeIn();
    }

    async UniTask FadeOut()
    {
        while (_transitionCanvasGroup.alpha > 0)
        {
            _transitionCanvasGroup.alpha = Mathf.Lerp(1, 0, _transitionDuration);
            await UniTask.Yield();
        }
    }

    async UniTask FadeIn()
    {
        while(_transitionCanvasGroup.alpha < 1)
        {
            _transitionCanvasGroup.alpha = Mathf.Lerp(0, 1, _transitionDuration);
            await UniTask.Yield();
        }
    }
}
