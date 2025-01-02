using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneBase : SingletonBase<SceneBase>
{
    public LoadSceneMode LoadSceneMode => IsadditiveScene ? LoadSceneMode.Additive : LoadSceneMode.Single;

    public abstract bool IsadditiveScene { get; }
    public abstract IEnumerator OnStart();
    public abstract IEnumerator OnEnd();
}
