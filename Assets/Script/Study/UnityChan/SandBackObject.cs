using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBackObject : MonoBehaviour, IDamage
{
    public float currentHP;
    public float maxHp;

    public SandbackVisualData[] visualDatas;

    public void ApplyDamage(float getDamage)
    {
        currentHP -= getDamage;

        Renderer selfRenderer = GetComponent<Renderer>();
        if (selfRenderer != null)
        {
            int visualDataIndex = 0;
            float currentHpRatio = currentHP/(float)maxHp;
            for (int i = 0; i < visualDatas.Length; i++)
            {
                Debug.Log(currentHpRatio + " " + i);
                if(currentHpRatio>= visualDatas[i].rangeMin && currentHpRatio < visualDatas[i].rangeMax)
                {
                    Debug.Log("ÃÖÁ¾ " + i);
                    visualDataIndex = i;
                }
            }
            selfRenderer.material.color = visualDatas[visualDataIndex].color;
        }

        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
