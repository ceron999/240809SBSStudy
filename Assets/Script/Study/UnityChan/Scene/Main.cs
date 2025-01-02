using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    None, 
    Empty,
    Title, 
    Ingame,
}

// 게임을 실행했을 때, 가장 먼저 실행되는 Entry 역할을 하는 클래스
public class Main : SingletonBase<Main>
{
    // #1. 현재 게임의 Scene을 관리하는 SceneController 역할을 수행
    // #2. 게임의 초기화 구역을 담당하는 역할

    public SceneType CurrentSceneType => currentSceneType;

    [SerializeField] private SceneType currentSceneType = SceneType.None;
    public SceneBase CurrentSceneController => currentSceneController;
    public bool IsOnProgressSceneChanging { get; private set; } = false;

    private SceneBase currentSceneController = null;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        StartCoroutine(MainSystemInitialize());
    }

    IEnumerator MainSystemInitialize()
    {
        yield return null;

        // TODO : 각 종 필요한 시스템을 초기화
        // TODO : 첫 실행되는 씬으로 전환
        ChangeScene(SceneType.Title);
    }

    public void SystemQuit()
    {
        // 게임 종료 기능
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void ChangeScene(SceneType sceneType, System.Action sceneLoadedCallback = null)
    {
        if (currentSceneType == sceneType)
            return;

        switch (sceneType)
        {
            case SceneType.Title:
                ChangeScene<TitleScene>(sceneType, sceneLoadedCallback);
                break;
            case SceneType.Ingame:
                ChangeScene<IngameScene>(sceneType, sceneLoadedCallback);
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    private void ChangeScene<T>(SceneType sceneType, System.Action sceneLoadedCallback = null) where T : SceneBase
    {
        if (IsOnProgressSceneChanging)
            return;

        StartCoroutine(ChangeSceneAsync<T>(sceneType, sceneLoadedCallback));
    }

    private IEnumerator ChangeSceneAsync<T>(SceneType sceneType, System.Action sceneLoadedCallback = null) where T : SceneBase
    {
        IsOnProgressSceneChanging = true;

        // TODO : Show Loading UI
        var loadingUI = UIManager.Show<LoadingUI>(UIList.LoadingUI);
        loadingUI.SetProgress(0);

        if (currentSceneController != null)
        {
            yield return currentSceneController.OnEnd();
            Destroy(currentSceneController.gameObject);
        
        }

        loadingUI.SetProgress(0.1f);
        yield return null;

        AsyncOperation emtpyOperation = SceneManager.LoadSceneAsync(SceneType.Empty.ToString(), LoadSceneMode.Single);
        yield return new WaitUntil(() => emtpyOperation.isDone);

        loadingUI.SetProgress(0.3f);
        yield return null;

        GameObject sceneGo = new GameObject(typeof(T).Name);
        sceneGo.transform.parent = transform;
        currentSceneController = sceneGo.AddComponent<T>();
        currentSceneType = sceneType;

        yield return StartCoroutine(currentSceneController.OnStart());

        loadingUI.SetProgress(1f);
        yield return null;

        // TODO : Hide Loading UI
        UIManager.Hide<LoadingUI>(UIList.LoadingUI);

        IsOnProgressSceneChanging = false;
        sceneLoadedCallback?.Invoke();
    }
}

