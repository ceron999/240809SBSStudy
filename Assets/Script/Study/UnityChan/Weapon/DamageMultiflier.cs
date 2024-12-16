using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMultiflier : MonoBehaviour
{
    // [] : 적은 부분은 Attribute 라고 부름
    // 변수 명 뒤에 get; set; 하고 붙은것들은 Property 라고 부름
    // get; set;이 없다면 일반 멤버 변수[:field] 라고 부름

    [field: SerializeField] public float DamageMultiplier { get; set; } = 1.0f;
}