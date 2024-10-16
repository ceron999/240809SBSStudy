using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingUI : MonoBehaviour
{
    public Image loadingBar;
    public TextMeshProUGUI loadingText;

    public void SetProgress(float progress)
    {
        loadingBar.fillAmount = progress;
        loadingText.text = $"{progress * 100}%";
    }
}
