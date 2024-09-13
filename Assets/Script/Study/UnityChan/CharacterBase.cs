using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    #region ĳ���� �̵� ���� ������
    [SerializeField] Rigidbody unityChanRigid;
    [SerializeField] Animator unityChanAnimator;

    // �̵� �ӵ� ���� ����
    [SerializeField] Vector2 movement;
    [SerializeField] Vector2 movementBlend;
    [SerializeField] float CharacterWalkSpeed = 1;
    [SerializeField] float CharacterRunSpeed = 10;

    public bool isRunning = false;
    public float runningBlend = 0;


    #endregion
    void Awake()
    {
        unityChanRigid = GetComponent<Rigidbody>();
        unityChanAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveAnimation();
    }

    void MoveAnimation()
    {
        Vector3 moveVec = new Vector3(movementBlend.x, 0, movementBlend.y);

        // �̵� �ӵ� ����
        float targetSpeed = isRunning ? CharacterRunSpeed : CharacterWalkSpeed;

        // �̵�
        transform.Translate(targetSpeed * moveVec *  Time.deltaTime);

        // �̵� �ִϸ��̼� ����
        runningBlend =  Mathf.Lerp(runningBlend, isRunning ? 1f : 0f, Time.deltaTime * 10f);
        movementBlend = Vector2.Lerp(movementBlend, movement, Time.deltaTime * 10f);

        unityChanAnimator.SetFloat("Magnitude", movementBlend.magnitude);
        unityChanAnimator.SetFloat("Vertical", movementBlend.y);
        unityChanAnimator.SetFloat("Horizontal", movementBlend.x);
        unityChanAnimator.SetFloat("Running", runningBlend);
    }

    public void Move(Vector2 input)
    {
        movement = input;
    }

    public void SetRunning(bool isLeftShifPressed)
    {
        isRunning = isLeftShifPressed;
    }
}
