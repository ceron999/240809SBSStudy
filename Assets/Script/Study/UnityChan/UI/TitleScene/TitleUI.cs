using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleUI : UIBase
{
    public void OnClickGameStartButton()
    {
        Main.Singleton.ChangeScene(SceneType.Ingame);
    }

    public void OnClickExitButton()
    {
        Main.Singleton.SystemQuit();
    }
}
