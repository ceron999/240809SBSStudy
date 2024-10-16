using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Thread thread = new Thread(Run);
        thread.Start();
    }

    void Run()
    {
        Debug.LogFormat("Thread#{0}: Ω√¿€", Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(1000);
        Debug.LogFormat("Thread#{0}: ≥°", Thread.CurrentThread.ManagedThreadId);
    }
}
