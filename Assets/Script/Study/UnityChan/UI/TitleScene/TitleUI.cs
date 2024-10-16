using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleUI : MonoBehaviour
{
    public void OnClickGameStartButton()
    {
        StartCoroutine(AsyncGameSceneLoad());
    }

    IEnumerator AsyncGameSceneLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        LoadingUI.Instance.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            LoadingUI.Instance.SetProgress(operation.progress / 0.9f);
            yield return null;
        }

        SceneManager.UnloadSceneAsync("TitleScene");
        LoadingUI.Instance.gameObject.SetActive(false);
    }

    public void OnClickExitButton()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
