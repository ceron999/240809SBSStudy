using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : MonoBehaviour
{
    #region Singleton
    public static CustomNetworkManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void StartHost()
    {
        Debug.Log("Start Host");
    }

    public void StartServer()
    {
        Debug.Log("Start Server");
    }

    public void StartClient()
    {
        Debug.Log("Start Client");
    }
}
