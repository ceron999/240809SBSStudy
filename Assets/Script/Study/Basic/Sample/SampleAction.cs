using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class TestEventArgs : EventArgs
{
    public string className;
    public TestEventArgs(string className)
    {
        this.className = className;
    }
}

public class SampleAction : MonoBehaviour
{
    /// <summary>
    /// delegate : 함수 원형이 필요함
    /// action : 함수 원형이 필요하진 않음 -> 반환형이 void만 가능
    /// </summary>
    //public delegate void ActionEvent();

    //public ActionEvent num1Action;
    //public ActionEvent num2Action;
    //public ActionEvent num3Action;

    public Action num1Action;
    public Action num2Action;
    public Action num3Action;

    [Header("테스트용 변수들")]
    public Event testEvent;
    public EventHandler testEventHandler;
    public event EventHandler testEventHandlerAndEvent;
    public event Action testEventAction;

    public Func<int> testFunc;
    public event Func<int> testEventFunc;
    public event EventHandler<EventArgs> testEventHandlerWithGeneric;

    /// <summary>
    /// Q1. Action과 event의 차이는?
    /// Q2. event와 Event의 차이는?          ->
    /// Q3. Event와 EventHandler의 차이는?   -> 파라미터 차이
    /// Q4. Event와 Func<>의 차이는?         ->
    /// </summary>

    /// <summary>
    /// event : delegate를 이벤트 목적으로 사용할 때 선언
    ///         event로 선언된 델리게이트는 외부 클래스에서 실행 불가
    ///         구독 및 해지 가능
    /// </summary>


    private void Start()
    {
        //num1Action += ActionRealFunction1;
        //num2Action += ActionRealFunction2;
        //num3Action += ActionRealFunction3;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            num1Action?.Invoke();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            num2Action?.Invoke();
        }
        else if( Input.GetKeyDown(KeyCode.Alpha3))
        {
            num3Action?.Invoke();
        }

        else if ( Input.GetKeyDown(KeyCode.Q))
        {
            // Event는 Invoke 안되네
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            // EventHandler는 object, EventArgs가 파라미터로 들어가는 듯
            // 얘 미리 지정된 Delegate라는디?
            // (object sender, EventArgs e) -> sender : 이벤트 실행시키는 객체
            //                              -> e : 실행할 떄 전하고 싶은 정보가 있는 경우 사용(없으면 EventArgs.Empty)
            //                              -> e 추가하고 싶으면 class : EventArgs 상속시키고 사용
            testEventHandler?.Invoke(this, null);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            testEventHandlerAndEvent?.Invoke(this, null);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            testEventAction?.Invoke();
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            testFunc?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            testEventFunc?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            testEventHandlerWithGeneric?.Invoke(this, null);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }
}
