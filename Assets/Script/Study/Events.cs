using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SampleCharacterBase
{
    public delegate void DamagedCallback(string name, float damage, float remainHp);
    public event DamagedCallback OnDamaged;

    public float hp;
    public string name;

    public void ApplyDamage(float damage)
    {
        hp -= damage;

        OnDamaged?.Invoke(name, damage, hp);
    }
}

public class Events : MonoBehaviour
{
    public SampleCharacterBase characterA;
    public SampleCharacterBase characterB;

    private void Start()
    {
        characterA = new SampleCharacterBase();
        characterB = new SampleCharacterBase();

        characterA.hp = 300;
        characterB.hp = 500;

        characterA.name = "characterA";
        characterB.name = "characterB";

        characterA.OnDamaged += OnCharacterDamaged;
        characterB.OnDamaged += OnCharacterDamaged;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            characterA.ApplyDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            characterB.ApplyDamage(10);
        }
    }

    public void OnCharacterDamaged(string name, float damage, float remainHp)
    {
        Debug.LogFormat("damage : {0}, Remain Hp : {1}, characterName : {2}", damage, remainHp, name);
    }
}
