using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataModel : SingletonBase<GameDataModel>
{
    [field: SerializeField] public CharacterStatData PlayerCharacterStatData { get; private set; }

    public void Initialize()
    {
        // For TEST - Save Character StatData
        CharacterDataDTO sampleData = new CharacterDataDTO()
        {
            HP = 100,
            SP = 10,
            RunSpeed = 1.5f,
            RunStaminaCost = 1f,
            StaminaRecoverySpeed = 0.5f,
            WalkSpeed = 1f,
        };

        string toJson = JsonUtility.ToJson(sampleData, true);
        FileManager.WriteFileFromString("Assets/Resources/Json/sampleData.json", toJson);

        LoadPlayerCharacterStatData();
    }

    public void LoadPlayerCharacterStatData()
    {
        TextAsset sampleDataText = Resources.Load<TextAsset>("Json/sampleData");
        
        var loadedDataDTO = JsonUtility.FromJson<CharacterDataDTO>(sampleDataText.text);

        var newRuntimeData = ScriptableObject.CreateInstance<CharacterStatData>();
        newRuntimeData.CharacterData = loadedDataDTO;
        PlayerCharacterStatData = newRuntimeData;
    }
}
