using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    #region Collision Code
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision != null)
    //    {
    //        Debug.Log("Col Enter : " + collision.gameObject.name);
    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision != null)
        {
            Debug.Log("Col Stay : " + collision.gameObject.name);
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision != null)
    //    {
    //        Debug.Log("Col Exit : " + collision.gameObject.name);
    //    }
    //}
    #endregion


    #region Trigger Code
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other != null)
    //    {
    //        Debug.Log("Trigger Enter : " + other.gameObject.name);
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other != null)
    //    {
    //        Debug.Log("Trigger Stay : " + other.gameObject.name);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other != null)
    //    {
    //        Debug.Log("Trigger Exit : " + other.gameObject.name);
    //    }
    //}
    #endregion
}
