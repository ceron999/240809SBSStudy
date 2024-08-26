using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleClass : MonoBehaviour
{
    public SampleDataClass sampleA;
    public SampleDataStruct sampleB;

    [SerializeField]
    Transform objTransform;
    [SerializeField]
    Rigidbody objRigid;

    // Start is called before the first frame update
    void Start()
    {
        //objTransform = this.transform;
        //objRigid = GetComponent<Rigidbody>();

        sampleA.sampleValue = 10;
        sampleB.sampleValue = 20;

        sampleA.SampleFunction();
        sampleB.SampleFunction();
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
    }
}

[System.Serializable]
public struct SampleDataStruct
{
    public int sampleValue;

    public void SampleFunction()
    {
        Debug.Log(sampleValue);
    }
}

[System.Serializable]
public class SampleDataClass
{
    public int sampleValue;

    public void SampleFunction()
    {
        Debug.Log(sampleValue);
    }
}