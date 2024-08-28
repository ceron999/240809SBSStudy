using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

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
    //드라이브/HTTPPrec/TestTxt.txt ID
    public string testTxtUrl = "https://docs.google.com/document/d/1BiQcxw7d1HJbXElfWERpjZhz-XdvV6OpXiDMQvtEa5M/edit?usp=sharing";
    //드라이브/HTTPPrec/ChickenImg.png ID
    public string testImageUrl = "https://drive.google.com/uc?export=download&id=1u3mN0EZe1qlNi8sp6lEmuh2zKBnT3MZZ";
    #endregion
    
    #region UI
    [SerializeField]
    Image httpImage;
    [SerializeField]
    TextMeshProUGUI httpText;
    #endregion

    private void Awake()
    {
        httpImage = GameObject.Find("HTTPImage").GetComponent<Image>();
        httpText = GameObject.Find("HTTPText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        #region Img Load
        ////GET Prectice
        Debug.Log("drive/Chicken URL");

        //Image  생성
        StartCoroutine(GetImage(testImageUrl));
        #endregion

        #region Text Load
        //GET Prectice
        Debug.Log("drive/TestTxt URL");

        //Text 생성
        StartCoroutine(GetText(url));
        #endregion
    }

    #region GET Functions
    //1. 그냥 URL 들어가기
    IEnumerator Get(string url)
    {
        //내 구글 드라이브 들어가는 방법
        UnityWebRequest wr = UnityWebRequest.Get(url);

        yield return wr.SendWebRequest();

        if (wr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wr.error);
        }
        else
        {
            Debug.Log(wr.downloadHandler.text);

            byte[] results = wr.downloadHandler.data;
        }
    }

    //2. 드라이브/HTTPPrec/TestTxt.txt 받아오기
    IEnumerator GetText(string url)
    {
        UnityWebRequest wr = UnityWebRequest.Get(url);
        
        yield return wr.SendWebRequest();

        if (wr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wr.error);
            Debug.Log(wr.downloadHandler.text);
        }
        else
        {
            //httpText.text = wr.downloadHandler.text;
            Debug.Log(wr.downloadHandler.text);
        }
    }

    //3. 드라이브/HTTPPrec/Chicken.png 받아오기
    IEnumerator GetImage(string url)
    {
        UnityWebRequest wr = UnityWebRequestTexture.GetTexture(url);

        yield return wr.SendWebRequest();

        if (wr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wr.error);
        }
        else
        {
            Debug.Log("Img Success");

            //texture to sprite
            Texture2D t = ((DownloadHandlerTexture)wr.downloadHandler).texture;
            Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.zero, 1f);
            httpImage.sprite = s;
        }
        #endregion
    }
}
