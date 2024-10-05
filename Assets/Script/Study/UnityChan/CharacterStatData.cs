using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character stat Data", menuName = "Character/Character Stat Data")]
public class CharacterStatData : ScriptableObject
{
    public float RunSpeed = 2.5f;
    public float WalkSpeed = 1f;
}