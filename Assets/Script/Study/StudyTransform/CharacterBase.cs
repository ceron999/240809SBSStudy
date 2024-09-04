using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public float MaxHp 
    { 
        get { return maxHp; }
        set { maxHp = value; }
    }
    private float maxHp;

    public float CurrHp
    {
        get { return currHp; }
        set { currHp = value; }
    }
    private float currHp;

    public float[] arr;

    private void Start()
    {
        maxHp = 100;
        currHp = 50;
        arr = new float[2];
        arr[0] = maxHp;
        arr[1] = currHp;
    }

    private void Update()
    {
        arr[0] = maxHp;
        arr[1] = currHp;
    }

    public void Heal(float healAmount)
    {
        if (currHp + healAmount >= maxHp)
        {
            currHp = maxHp;
        }
        else
        {
            currHp += healAmount;
        }
    }
}
