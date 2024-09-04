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
    /// delegate : �Լ� ������ �ʿ���
    /// action : �Լ� ������ �ʿ����� ���� -> ��ȯ���� void�� ����
    /// </summary>
    //public delegate void ActionEvent();

    //public ActionEvent num1Action;
    //public ActionEvent num2Action;
    //public ActionEvent num3Action;

    public Action num1Action;
    public Action num2Action;
    public Action num3Action;

    [Header("�׽�Ʈ�� ������")]
    public Event testEvent;
    public EventHandler testEventHandler;
    public event EventHandler testEventHandlerAndEvent;
    public event Action testEventAction;

    public Func<int> testFunc;
    public event Func<int> testEventFunc;
    public event EventHandler<EventArgs> testEventHandlerWithGeneric;

    /// <summary>
    /// Q1. Action�� event�� ���̴�?
    /// Q2. event�� Event�� ���̴�?          ->
    /// Q3. Event�� EventHandler�� ���̴�?   -> �Ķ���� ����
    /// Q4. Event�� Func<>�� ���̴�?         ->
    /// </summary>

    /// <summary>
    /// event : delegate�� �̺�Ʈ �������� ����� �� ����
    ///         event�� ����� ��������Ʈ�� �ܺ� Ŭ�������� ���� �Ұ�
    ///         ���� �� ���� ����
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
            // Event�� Invoke �ȵǳ�
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            // EventHandler�� object, EventArgs�� �Ķ���ͷ� ���� ��
            // �� �̸� ������ Delegate��µ�?
            // (object sender, EventArgs e) -> sender : �̺�Ʈ �����Ű�� ��ü
            //                              -> e : ������ �� ���ϰ� ���� ������ �ִ� ��� ���(������ EventArgs.Empty)
            //                              -> e �߰��ϰ� ������ class : EventArgs ��ӽ�Ű�� ���
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
