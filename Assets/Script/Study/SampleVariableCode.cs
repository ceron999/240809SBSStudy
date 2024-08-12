using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleVariableCode : MonoBehaviour
{
    int sampleA = 10;
    int sampleB = 20;

    void Start()
    {
        int localA = 30;
        int localB = 40;

        Sum(sampleA, sampleB);
        Sum(localA, localB);
    }
    private void Update()
    {
        if(sampleA > sampleB)
        {
            Debug.Log("A > B");
        }
        else
        {
            Debug.Log("A <= B");
        }
    }


    int MyFunction(int a, int b)
    {
        return a + b;
    }

    void Sum(int a, int b)
    {
        int result = a + b;
        Debug.Log(result);
    }
}