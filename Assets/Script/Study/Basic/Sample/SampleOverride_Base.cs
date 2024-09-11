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
    /// 1. virtual
    ///     virtual 키워드는 메서드,속성, 인덱서 또는 이벤트 선언을 한정하는데 사용됩니다.
    ///     파생 클래스에서 필요에 따라서 재정의(override) 할 수 있지만  
    ///     필수적으로 재정의 할 필요는 없습니다.
    ///     Virtual 한정자를 사용한 클래스는 완벽한 기능을 제공할 수 있습니다.
    ///
    /// 2. abstract
    ///     abstract 키워드를 사용하면 불완전하여 파생 클래스에서 구현해야하는 
    ///     클래스 및 클래스 멤버를 만들수 있습니다.
    ///     추상클래스의 사용 목적은 여러개의 파생 클래스에서 공유할 기본 클래스의 공통적인 정의를 
    ///     제공하는 것입니다. 추상 클래스는 인스턴스화할 수 없습니다.
    /// 
    /// 3. intertace
    ///     인터페이스는 abstract와 비슷하지만 멤버필드(변수)를 사용할 수 없습니다. 
    ///     대신에 프로퍼티는 사용이 가능합니다.
    ///     인터페이스는 보통 여러클래스에 공통적인 기능을 추가하기 위해서 사용합니다.
    /// </summary>
    public virtual void Attack()
    {
        Debug.Log("BaseAttack");
    }
}
