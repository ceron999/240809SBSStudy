using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public Animator characterAnimator;
    public CharacterStatData characterStat;

    public Vector2 movement;
    public Vector2 movementBlend;
    public float rotation = 0f;
    public bool isRunning = false;
    public float runningBlend;

    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGround();
        FreeFall();

        Vector3 moveVec = new Vector3(movementBlend.x, verticalVelocity, movementBlend.y);

        float targetSpeed = isRunning ? characterStat.RunSpeed : characterStat.WalkSpeed;
        transform.Translate(targetSpeed * moveVec * Time.deltaTime, Space.Self);

        runningBlend = Mathf.Lerp(runningBlend, isRunning ? 1f : 0f, Time.deltaTime * 10f);

        // Movement Blend : 를 이용하는 이유는 애니메이션에 적용할 parameter 값을 갑자기 튀지 않도록
        // 하기 위해서 중간(보간)값을 구해서 적용을 했음
        movementBlend = Vector2.Lerp(movementBlend, movement, Time.deltaTime * 10f);

        characterAnimator.SetFloat("Magnitude", movementBlend.magnitude);
        characterAnimator.SetFloat("Horizontal", movementBlend.x);
        characterAnimator.SetFloat("Vertical", movementBlend.y);
        characterAnimator.SetFloat("RunningBlend", runningBlend);
    }

    public void Move(Vector2 input)
    {
        movement = input;
    }


    public void Rotate(float angle)
    {
        rotation += angle;
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void SetRunning(bool isRun)
    {
        isRunning = isRun;
    }

    [Header("점프 세팅")]
    private bool isGrounded = false;
    private float jumpForce = 3f;
    private float verticalVelocity = 0f;
    private float gravity = -9.8f;
    private float jumpTimeDelta = 0f;
    private float jumpTimeout = 0.3f;
    private const float JUMP_DELAY = 3f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    public void Jump()
    {
        if (isGrounded && jumpTimeDelta <= 0f)
        {
            verticalVelocity = jumpForce;
            jumpTimeDelta = JUMP_DELAY;
            jumpTimeout = 0.3f;
        }
    }

    public void FreeFall()
    {
        jumpTimeDelta -= Time.deltaTime;
        jumpTimeout -= Time.deltaTime;

        if (jumpTimeDelta <= 0f) jumpTimeDelta = 0f;

        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else
        {
            if (jumpTimeout <= 0f)
            {
                verticalVelocity = 0f;
            }
        }
    }

    public void CheckGround()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundLayer);
    }
}