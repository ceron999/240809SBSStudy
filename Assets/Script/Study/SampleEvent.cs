using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SampleEvent : MonoBehaviour
{
    public delegate void DamageCallBack(float damage);
    public event EventHandler testEventHandler;
    public event DamageCallBack OnDamaged;

    public float Hp;
    public float damage;

    public Button btn;
    public UnityEvent testUnityEvent;
    public Action actions;

    private void Start()
    {
        Hp = 100;
        damage = 5;

        //1. event callback
        OnDamaged += DamageCallBackFunction;
        OnDamaged += DamageCallBackFunction2;
        OnDamaged += DamageCallBackFunction3; 



        //2. UnityEvent
        testUnityEvent = new UnityEvent();
        testUnityEvent.AddListener(UnityEventFunction);
        testUnityEvent.AddListener(() => UnityEventFunctionWithParameter(1));

        //3. Btn
        btn.onClick.AddListener(() => OnDamaged(damage));

        //4. Action
        actions += ActionFunction;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            ApplyDamage(damage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            testUnityEvent?.Invoke();
        }

    }

    #region Delegate 콜백 함수
    public void ApplyDamage(float damage)
    {
        Hp -= damage;
        OnDamaged(damage);
    }

    public void DamageCallBackFunction(float damage)
    {
        Debug.Log("Taken Damage : " + damage);
    }
    public void DamageCallBackFunction2(float damage)
    {
        Debug.Log("Taken Damage2 : " + damage);
    }
    public void DamageCallBackFunction3(float damage)
    {
        Debug.Log("Taken Damage3 : " + damage);
    }
    public void DamageCallBackFunction4(float damage, float damage2)
    {
        Debug.Log("Taken Damage3 : " + damage);
    }
    #endregion

    #region UnityEvent
    public void TriggerEvent()
    {
        testUnityEvent?.Invoke();
    }

    private void UnityEventFunction()
    {
        Debug.Log("TestUnityEvent");
    }
    private void UnityEventFunctionWithParameter(int test1)
    {
        Debug.Log("TestUnityEventWithParameter : " + test1);
    }

    #endregion

    #region Action
    public void TriggerAction()
    {
        actions?.Invoke();
    }

    private void ActionFunction()
    {
        Debug.Log("TestActionFunction");
    }
    #endregion
}
