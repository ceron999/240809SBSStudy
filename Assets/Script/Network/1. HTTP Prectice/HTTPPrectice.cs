using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

/// <summary>
/// ��ǥ: ������ ������ �޾Ƽ� ����(�ؽ�Ʈ�� �̹��� �޾ƿ��� �켱)
/// ���Ŀ� ä�� UI ���� ��
/// </summary>


public class HTTPPrectice : MonoBehaviour
{
    #region url ����Ʈ
    [Header("����̺� URL ���")]

    //�� ���� ����̺�/HTTPPrec ����
    public string url = "https://drive.google.com/drive/folders/1O8s_BDiMeIZpGsO4Ed92-Xgp39iQXZvO?usp=sharing";
    //����̺�/HTTPPrec/TestTxt.txt ID
    public string testTxtUrl = "https://docs.google.com/document/d/1BiQcxw7d1HJbXElfWERpjZhz-XdvV6OpXiDMQvtEa5M/edit?usp=sharing";
    //����̺�/HTTPPrec/ChickenImg.png ID
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

        //Image  ����
        StartCoroutine(GetImage(testImageUrl));
        #endregion

        #region Text Load
        //GET Prectice
        Debug.Log("drive/TestTxt URL");

        //Text ����
        StartCoroutine(GetText(url));
        #endregion
    }

    #region GET Functions
    //1. �׳� URL ����
    IEnumerator Get(string url)
    {
        //�� ���� ����̺� ���� ���
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

    //2. ����̺�/HTTPPrec/TestTxt.txt �޾ƿ���
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

    //3. ����̺�/HTTPPrec/Chicken.png �޾ƿ���
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
