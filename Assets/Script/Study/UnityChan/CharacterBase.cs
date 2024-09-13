using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    #region 캐릭터 이동 관련 데이터
    [SerializeField] Rigidbody unityChanRigid;
    [SerializeField] Animator unityChanAnimator;

    // 이동 속도 관련 변수
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

        // 이동 속도 설정
        float targetSpeed = isRunning ? CharacterRunSpeed : CharacterWalkSpeed;

        // 이동
        transform.Translate(targetSpeed * moveVec *  Time.deltaTime);

        // 이동 애니메이션 설정
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
