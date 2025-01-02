using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataDTO : MonoBehaviour
{
    
}

public class PlayerDataDTO : UserDataDTO
{
    public float HP = 1000f;
    public float SP = 100f;

    public float RunSpeed = 2.5f;
    public float WalkSpeed = 1f;

    public float RunStaminaCost = 3f;
    public float StaminaRecoverySpeed = 2f;
}
