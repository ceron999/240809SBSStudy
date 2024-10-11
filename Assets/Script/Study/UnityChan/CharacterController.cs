using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    CharacterBase character;

    public LayerMask interactionLayer;
    IInteractable[] interactableObjects;
    public InteractionUI interactionUI;

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    private void Start()
    {
        InputSystem.Instance.OnClickSpace += CommandJump;
        InputSystem.Instance.OnClickLeftMouseBtn += CommandAttack;
        InputSystem.Instance.OnClickInteract += CommandInteract;
    }

    private void Update()
    {
        CheckOverlapInteractionObject();

        character.Move(InputSystem.Instance.Movement);
        character.Rotate(InputSystem.Instance.Look.x);
        character.SetRunning(InputSystem.Instance.IsLeftShift);
    }

    void CommandJump()
    {
        character.Jump();
    }

    void CommandAttack()
    {
        character.Attack();
    }

    void CommandInteract()
    {
        if (interactableObjects != null &&  interactableObjects.Length >0)
        {
            interactionUI.Execute();
            interactableObjects[0].Interact();
        }
    }

    public void CheckOverlapInteractionObject()
    {
        Collider[] overlappedObjects = Physics.OverlapSphere(character.transform.position, 2f, interactionLayer, QueryTriggerInteraction.Collide);

        List<IInteractable> interactables = new List<IInteractable>();
        for(int i = 0; i < overlappedObjects.Length; i++)
        {
            if (overlappedObjects[i].TryGetComponent(out IInteractable interaction))
            {
                interactables.Add(interaction);
                interaction.Interact();
            }
        }
        interactableObjects = interactables.ToArray();

        interactionUI.SetInteractableObjects(interactableObjects);
    }
}
