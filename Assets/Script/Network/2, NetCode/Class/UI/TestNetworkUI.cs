using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestNetworkUI : MonoBehaviour
{
    [SerializeField]
    Button hostBtn;
    [SerializeField]
    Button serverBtn;
    [SerializeField]
    Button clientBtn;

    private void Awake()
    {
        hostBtn = GameObject.Find("HostBtn").GetComponent<Button>();
        serverBtn = GameObject.Find("ServerBtn").GetComponent<Button>();
        clientBtn = GameObject.Find("ClientBtn").GetComponent<Button>();
    }

    private void Start()
    {
        hostBtn.onClick.AddListener
        (
            () =>
            {
                NetworkManager.Singleton.StartHost();
            }
        );

        serverBtn.onClick.AddListener
        (
            () =>
            {
                NetworkManager.Singleton.StartServer();
            }
        );

        clientBtn.onClick.AddListener
        (
            () =>
            {
                NetworkManager.Singleton.StartClient();
            }
        );
    }
}
