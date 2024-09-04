using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleOverride_Base : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }
    /// <summary>
    /// virtual :   내부에 함수 쓰기 가능
    ///             
    /// </summary>
    public virtual void Attack()
    {
        Debug.Log("BaseAttack");
    }
}
