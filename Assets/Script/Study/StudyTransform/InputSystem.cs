using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InputSystem : MonoBehaviour
{
    public float speed;


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
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //¿Þ
            transform.position += transform.right * (-1) * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //µÚ
            transform.position += transform.forward * (-1) * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //¿À
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
    #endregion

}
