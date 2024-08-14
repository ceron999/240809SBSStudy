using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/// <summary>
/// 목표: 웹에서 데이터 받아서 정리(텍스트나 이미지 받아오기 우선)
/// 이후엔 채팅 UI 구현 등
/// </summary>


public class HTTPPrectice : MonoBehaviour
{
    #region url 리스트
    [Header("드라이브 URL 덩어리")]
    //내 구글 드라이브/HTTPPrec 접근
    public string url = "https://drive.google.com/drive/folders/1O8s_BDiMeIZpGsO4Ed92-Xgp39iQXZvO?usp=sharing";
    //드라이브/HTTPPrec/TestTxt.txt URL
    public string testTxtUrl = "https://docs.google.com/document/d/1aJE7VBV1stfQmY3isnxOXII_mBFuX-3n49kPImaLDsY/edit?usp=drive_link";
    //드라이브/HTTPPrec/TestTxt.txt URL
    public string testImageUrl = "https://drive.google.com/file/d/1u3mN0EZe1qlNi8sp6lEmuh2zKBnT3MZZ/view?usp=drive_link";
    #endregion

    #region UI
    [SerializeField]
    Image httpImage;
    #endregion

    private void Awake()
    {
        httpImage = GameObject.Find("HTTPImage").GetComponent<Image>();
    }

    private void Start()
    {
        //GET Prectice
        Debug.Log("drive/Chicken URL");
        StartCoroutine(GetImage(testImageUrl));
    }

    #region GET Functions
    //1. 그냥 URL 들어가기
    IEnumerator Get(string url)
    {
        //내 구글 드라이브 들어가는 방법
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            byte[] results = www.downloadHandler.data;
        }
    }

    //2. 드라이브/HTTPPrec/TestTxt.txt 받아오기
    IEnumerator GetText(string url)
    {
        yield return null;
    }

    //3. 드라이브/HTTPPrec/Chicken.png 받아오기
    IEnumerator GetImage(string url)
    {
        UnityWebRequest wr = new UnityWebRequest(url);
        DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
        wr.downloadHandler = texDl;

        yield return wr.SendWebRequest();

        if (wr.result == UnityWebRequest.Result.Success)
        {
            Texture2D t = texDl.texture;
            Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height),
                Vector2.zero, 1f);
            httpImage.sprite = s;

        }
        #endregion
    }
}
