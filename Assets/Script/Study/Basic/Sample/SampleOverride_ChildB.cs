using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleOverride_ChildB : SampleOverride_Base
{
    public override void Attack()
    {
        //base.Attack();
        Debug.Log("AttackB");
    }
}
