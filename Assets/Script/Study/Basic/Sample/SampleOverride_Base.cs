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
    /// virtual :   ���ο� �Լ� ���� ����
    ///             
    /// </summary>
    public virtual void Attack()
    {
        Debug.Log("BaseAttack");
    }
}
