using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameScene : SceneBase
{
    public override bool IsadditiveScene => false;

    public override IEnumerator OnStart()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneType.Ingame.ToString(), LoadSceneMode);
        while (!async.isDone)
        {
            yield return null;
        }

        UIManager.Show<IngameUI>(UIList.IngameUI);
        UIManager.Show<MinimapUI>(UIList.MinimapUI);
        UIManager.Show<CrosshairUI>(UIList.CrosshairUI);
    }

    public override IEnumerator OnEnd()
    {
        UIManager.Hide<IngameUI>(UIList.IngameUI);
        UIManager.Hide<MinimapUI>(UIList.MinimapUI);
        UIManager.Hide<CrosshairUI>(UIList.CrosshairUI);

        yield return null;
    }
}
