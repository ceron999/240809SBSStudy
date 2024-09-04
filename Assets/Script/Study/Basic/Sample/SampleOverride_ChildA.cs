using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleOverride_ChildA : SampleOverride_Base
{
    public override void Attack()
    {
        //base.Attack();
        Debug.Log("AttackA");
    }
}
