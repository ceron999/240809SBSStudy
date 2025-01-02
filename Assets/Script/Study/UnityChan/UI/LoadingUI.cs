using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingUI : UIBase
{
    public static LoadingUI Instance { get; private set; }

    public Image loadingBar;
    public TextMeshProUGUI loadingText;

    private void OnEnable()
    {
        loadingBar.fillAmount = 0;
        loadingText.text = $"0 %";
    }

    public void SetProgress(float progress)
    {
        loadingBar.fillAmount = progress;
        loadingText.text = $"{progress * 100}%";
    }
}
