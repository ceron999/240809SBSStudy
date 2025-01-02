using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataDTO
{
    
}

public class CharacterDataDTO : GameDataDTO
{
    public float HP = 1000f;
    public float SP = 100f;

    public float RunSpeed = 2.5f;
    public float WalkSpeed = 1f;

    public float RunStaminaCost = 3f;
    public float StaminaRecoverySpeed = 2f;
}