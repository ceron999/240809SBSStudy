using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : SceneBase
{
    public override bool IsadditiveScene => false;

    public override IEnumerator OnStart()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneType.Title.ToString(), LoadSceneMode);
        while(!async.isDone)
        {
            yield return null;

            float progress = async.progress / 0.9f;
            LoadingUI.Instance.SetProgress(progress);
        }

        UIManager.Show<TitleUI>(UIList.TitleUI);
    }

    public override IEnumerator OnEnd()
    {
        UIManager.Hide<TitleUI>(UIList.TitleUI);
        yield return null;
    }
}
