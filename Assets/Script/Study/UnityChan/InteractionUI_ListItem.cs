using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI_ListItem : MonoBehaviour
{
    public string InteractionText
    {
        set
        {
            ItemNameText.text = value;
        }
    }

    public bool IsShowShortcut
    {
        set
        {
            shortcut.SetActive(value);
        }
    }

    public TMPro.TextMeshProUGUI ItemNameText;
    public GameObject shortcut;

    public IInteractable InteractionData { get; set; }
    public void Execute()
    {
        InteractionData?.Interact();
    }
}
