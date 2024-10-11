using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    CharacterBase character;

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    private void Start()
    {
        InputSystem.Instance.OnClickSpace += CommandJump;
        InputSystem.Instance.OnClickLeftMouseBtn += CommandAttack;
    }

    private void Update()
    {
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
}
