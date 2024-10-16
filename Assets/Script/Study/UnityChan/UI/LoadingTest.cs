using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTest : MonoBehaviour
{
    public LoadingUI loadingUI;

    [Range(0f,1f)]
    public float progress = 0f;

    // Update is called once per frame
    void Update()
    {
        loadingUI.SetProgress(progress);
    }
}
