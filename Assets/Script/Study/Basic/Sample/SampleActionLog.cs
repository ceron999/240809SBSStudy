using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleActionLog : MonoBehaviour
{
    public SampleAction sampleAction;

    private void Awake()
    {
        sampleAction = GetComponent<SampleAction>();

        sampleAction.num1Action += ActionA;
        sampleAction.num2Action += ActionB;
        sampleAction.num3Action += ActionC;

        sampleAction.testEventHandler += ActionEventHandler;
    }

    void ActionA()
    {
        Debug.Log("Action A");
    }

    void ActionB()
    {
        Debug.Log("Action B");
    }

    void ActionC()
    {
        Debug.Log("Action C");
    }

    void ActionEventHandler(object getObj, EventArgs getEventArgs)
    {
        Debug.Log("Action EventHandler");
    }
}
