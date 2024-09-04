using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCoroutine : MonoBehaviour
{
    public string[] texts;
    public bool isShowNext = false;

    private void Start()
    {
        //StartCoroutine(DelayedShowText());
        StartCoroutine(DelayedShowText_2());
    }

    IEnumerator DelayedShowText_2()
    {
        Debug.Log("Hello World");

        yield return new WaitForSeconds(1f);

        Debug.Log("I am Unity");

        yield return new WaitForSeconds(1.5f);

        Debug.Log("Unity is So Bad");

        yield return new WaitUntil(() => isShowNext);

        Debug.Log("Unity is Not Bad");
    }

    IEnumerator DelayedShowText()
    {
        Debug.Log("Coroutine Start");

        for (int i = 0; i < texts.Length; i++)
        {
            yield return new WaitForSeconds(1f);

            Debug.Log(texts[i]);
        }
    }
}
