using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleDialogue2 : MonoBehaviour
{
    public int strikeCount = 5;
    public float currentHP = 100.0f;
    public float damageMin = 3.0f;
    public float damageMax = 10.0f;

    private void Start()
    {
        ShowRemainHP();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 1: 마우스 우 클릭(Right Button)
        {
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("공격 시작");
        for (int i = 0; i < strikeCount; i++)
        {
            float randomDamage = Random.Range(damageMin, damageMax);
            currentHP -= randomDamage;
            ShowRemainHP();
        }
        Debug.Log("공격 끝");
    }

    public void ShowRemainHP()
    {
        Debug.Log("남은 HP : " + currentHP);
    }
}

