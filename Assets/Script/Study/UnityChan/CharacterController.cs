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

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector2 input = new Vector2 (inputX, inputY);

        bool isLeftShifPressed = Input.GetKey(KeyCode.LeftShift);

        character.Move(input);
        character.SetRunning(isLeftShifPressed);
    }
}
