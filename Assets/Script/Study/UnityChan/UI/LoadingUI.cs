using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI Instance { get; private set; }

    public Image loadingBar;
    public TextMeshProUGUI loadingText;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);

        loadingText.text = $"{0}";
        loadingBar.fillAmount = 0;

        DontDestroyOnLoad(gameObject);
    }

    public void SetProgress(float progress)
    {
        loadingBar.fillAmount = progress;
        loadingText.text = $"{progress * 100}%";
    }
}
