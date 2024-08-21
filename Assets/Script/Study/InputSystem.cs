using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    #region Move Function
    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //¾Õ
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //¿Þ
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //µÚ
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //¿À
        }
    }
    #endregion
}
