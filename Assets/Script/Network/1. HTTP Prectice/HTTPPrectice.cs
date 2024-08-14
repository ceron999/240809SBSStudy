using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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
    //����̺�/HTTPPrec/TestTxt.txt URL
    public string testTxtUrl = "https://docs.google.com/document/d/1aJE7VBV1stfQmY3isnxOXII_mBFuX-3n49kPImaLDsY/edit?usp=drive_link";
    //����̺�/HTTPPrec/TestTxt.txt URL
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
    //1. �׳� URL ����
    IEnumerator Get(string url)
    {
        //�� ���� ����̺� ���� ���
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

    //2. ����̺�/HTTPPrec/TestTxt.txt �޾ƿ���
    IEnumerator GetText(string url)
    {
        yield return null;
    }

    //3. ����̺�/HTTPPrec/Chicken.png �޾ƿ���
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
