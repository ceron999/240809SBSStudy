using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBackObject : MonoBehaviour
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
            float currentHpRatio = currentHP/maxHp;
            for (int i = 0; i < visualDatas.Length; i++)
            {
                if(currentHpRatio>= visualDatas[i].rangeMin && currentHpRatio < visualDatas[i].rangeMax)
                {
                    visualDataIndex = i;
                }
                selfRenderer.material.color = visualDatas[i].color;
            }
        }

        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
