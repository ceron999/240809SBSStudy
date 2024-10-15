using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character stat Data", menuName = "Character/Character Stat Data")]
public class CharacterStatData : ScriptableObject
{
    public float MaxHP = 1000f;
    public float MaxSP = 100f;

    public float RunSpeed = 2.5f;
    public float WalkSpeed = 1f;
}