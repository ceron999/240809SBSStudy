using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using BenchmarkDotNet;
//using BenchmarkDotNet.Attributes;

public class SampleSwitch : MonoBehaviour
{
    public enum SampleSwitchType
    {
        // ÃÑ 27°³
        Null, SwitchA, SwitchB, SwitchC, SwitchD, 
        SwitchE, SwitchF, SwitchG, SwitchH, SwitchI, 
        SwitchJ, SwitchK, SwitchL, SwitchM, SwitchN,
        SwitchO, SwitchP, SwitchQ, SwitchR, SwitchS, 
        SwitchT, SwitchU, SwitchV, SwitchW, SwitchX,
        SwitchY, SwitchZ
    }

    public SampleSwitchType switchMode;

    //[ParamsAllValues]
    public SampleSwitchType currentKey;

    public Dictionary<SampleSwitchType, Action> dict;
    public int dest;

    //[GlobalSetup]
    void GlobalSetup()
    {
        dict = new Dictionary<SampleSwitchType, Action>();
        SampleSwitchType[] typeArr 
            = Enum.GetValues(typeof(SampleSwitchType)).Cast<SampleSwitchType>().ToArray();

        foreach(var e in typeArr)
        {
            dict.Add(e, Job);
        }
    }

    //[Benchmark]
    void IfElseFunc()
    {
        if (switchMode == SampleSwitchType.Null) Job();
        else if (switchMode == SampleSwitchType.SwitchA) Job();
        else if (switchMode == SampleSwitchType.SwitchB) Job();
        else if (switchMode == SampleSwitchType.SwitchC) Job();
        else if (switchMode == SampleSwitchType.SwitchD) Job();

        else if (switchMode == SampleSwitchType.SwitchE) Job();
        else if (switchMode == SampleSwitchType.SwitchF) Job();
        else if (switchMode == SampleSwitchType.SwitchG) Job();
        else if (switchMode == SampleSwitchType.SwitchH) Job();
        else if (switchMode == SampleSwitchType.SwitchI) Job();

        else if (switchMode == SampleSwitchType.SwitchJ) Job();
        else if (switchMode == SampleSwitchType.SwitchK) Job();
        else if (switchMode == SampleSwitchType.SwitchL) Job();
        else if (switchMode == SampleSwitchType.SwitchM) Job();
        else if (switchMode == SampleSwitchType.SwitchN) Job();

        else if (switchMode == SampleSwitchType.SwitchO) Job();
        else if (switchMode == SampleSwitchType.SwitchP) Job();
        else if (switchMode == SampleSwitchType.SwitchQ) Job();
        else if (switchMode == SampleSwitchType.SwitchR) Job();
        else if (switchMode == SampleSwitchType.SwitchS) Job();

        else if (switchMode == SampleSwitchType.SwitchT) Job();
        else if (switchMode == SampleSwitchType.SwitchU) Job();
        else if (switchMode == SampleSwitchType.SwitchV) Job();
        else if (switchMode == SampleSwitchType.SwitchW) Job();
        else if (switchMode == SampleSwitchType.SwitchX) Job();

        else if (switchMode == SampleSwitchType.SwitchY) Job();
        else if (switchMode == SampleSwitchType.SwitchZ) Job();
    }

    //[Benchmark(Baseline = true)]
    void SwitchFunc()
    {
        switch(currentKey)
        {
            case SampleSwitchType.Null: Job(); break;
            case SampleSwitchType.SwitchA: Job(); break;
            case SampleSwitchType.SwitchB: Job(); break;
            case SampleSwitchType.SwitchC: Job(); break;
            case SampleSwitchType.SwitchD: Job(); break;

            case SampleSwitchType.SwitchE: Job(); break;
            case SampleSwitchType.SwitchF: Job(); break;
            case SampleSwitchType.SwitchG: Job(); break;
            case SampleSwitchType.SwitchH: Job(); break;
            case SampleSwitchType.SwitchI: Job(); break;

            case SampleSwitchType.SwitchJ: Job(); break;
            case SampleSwitchType.SwitchK: Job(); break;
            case SampleSwitchType.SwitchL: Job(); break;
            case SampleSwitchType.SwitchM: Job(); break;
            case SampleSwitchType.SwitchN: Job(); break;

            case SampleSwitchType.SwitchO: Job(); break;
            case SampleSwitchType.SwitchP: Job(); break;
            case SampleSwitchType.SwitchQ: Job(); break;
            case SampleSwitchType.SwitchR: Job(); break;
            case SampleSwitchType.SwitchS: Job(); break;

            case SampleSwitchType.SwitchT: Job(); break;
            case SampleSwitchType.SwitchU: Job(); break;
            case SampleSwitchType.SwitchV: Job(); break;
            case SampleSwitchType.SwitchW: Job(); break;
            case SampleSwitchType.SwitchX: Job(); break;

            case SampleSwitchType.SwitchY: Job(); break;
            case SampleSwitchType.SwitchZ: Job(); break;
        }
    }

    //[Benchmark]
    void DictFunc()
    {
        dict[currentKey].Invoke();
    }

    void Job() => dest = (int)currentKey;
}
