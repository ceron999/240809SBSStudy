using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : MonoBehaviour, IInteractable
{
    public string InteractionMessage => "Open";

    public Transform pivot;
    public bool isOpened = false;


    void Update()
    {
        Quaternion targetRotation;
        if (isOpened)
        {
            targetRotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else
        {
            targetRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        pivot.localRotation = Quaternion.Slerp(pivot.localRotation, targetRotation, Time.deltaTime * 5);
    }

    public void Interact()
    {
        isOpened = !isOpened;
    }
}
