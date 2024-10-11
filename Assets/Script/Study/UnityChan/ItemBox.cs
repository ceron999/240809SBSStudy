using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable
{
    public string InteractionMessage => itemName;
    public string itemName;

    public void Interact()
    {
        Debug.Log("Get");

        Destroy(gameObject);
    }
}
