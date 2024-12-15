using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Muzzle_01,
    Muzzle_02,

    Impact_Wood,
    Impact_Metal,
    Impact_Concrete,
    Impact_Stone,
    Impact_Dirt,
}

[System.Serializable]
public class EffectData
{
    public EffectType effectType;
    public GameObject effectPrefab;
    public float lifeTime;
}

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; } = null;

    public List<EffectData> effectDataList = new List<EffectData>();

    private void Awake()
    {
        Instance = this;
    }

    public void CreateEffect(EffectType type, Vector3 position, Quaternion rotation)
    {
        var targetEffectData = effectDataList.Find(x => x.effectType == type);
        if (targetEffectData == null)
            return;

        var newEffect = Instantiate(targetEffectData.effectPrefab, position, rotation);
        newEffect.gameObject.SetActive(true);
        Destroy(newEffect, targetEffectData.lifeTime);
    }
}