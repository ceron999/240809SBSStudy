using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataModel : SingletonBase<UserDataModel>
{
    [field: SerializeField] public PlayerDataDTO playerData { get; private set; }
    //public void Start()
    //{
    //    // For TEST - Save Player Data
    //    SavePlayerData(new PlayerDataDTO()
    //    {
    //        health = 100,
    //        level = 1,
    //        name = "REDFORCE",
    //        position = new Vector3(10, 0, 30)
    //    });
    //}

    public void Initialize()
    {
        //LoadPlayerData();
    }

    public void SavePlayerData(PlayerDataDTO data)
    {
        string playerDataToJson = JsonUtility.ToJson(data, true);
        Debug.Log(playerDataToJson);

        PlayerPrefs.SetString(typeof(PlayerDataDTO).Name, playerDataToJson);
    }

    public void LoadPlayerData()
    {
        // TODO : Load Player Data To "PlayerData" Property
        string loadedPlayerDataString = PlayerPrefs.GetString(typeof(PlayerDataDTO).Name, string.Empty);

        playerData = JsonUtility.FromJson<PlayerDataDTO>(loadedPlayerDataString);
    }

}
