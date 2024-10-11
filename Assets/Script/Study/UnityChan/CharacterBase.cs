using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    #region 공격 데이터 
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, sensorRadius);

        //for(int i = 0; i< detectedObjects.Count; i++)
        //{
        //    Gizmos.color = Color.green;
        //    Vector3 startPos = transform.position + Vector3.up;
        //    Vector3 endPos = detectedObjects[i].transform.position;

        //    Gizmos.DrawLine(startPos, endPos);
        //}
    }

    public float attackRange = 3f;
    public float attackAngle = 80f;
    public float attackDamage = 10f;
    public LayerMask characterLayer;
    public LayerMask detectLayer;

    public Collider[] overlappedObjects;
    public List<Collider> detectedObjects = new List<Collider>();
    #endregion

    public Animator characterAnimator;
    public CharacterStatData characterStat;

    public Vector2 movement;
    public Vector2 movementBlend;
    public float rotation = 0f;
    public bool isRunning = false;
    public float runningBlend;

    [Header("점프 세팅")]
    private bool isGrounded = false;
    private float jumpForce = 3f;
    private float verticalVelocity = 0f;
    private float gravity = -9.8f;
    private float jumpTimeDelta = 0f;
    private float jumpTimeout = 0.3f;
    private const float JUMP_DELAY = 3f;
    public float groundCheckDistance = 0.1f;
    public float groundOffset = 0.1f;
    public LayerMask groundLayer;

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

    public void Jump()
    {
        if (isGrounded && jumpTimeDelta <= 0f)
        {
            verticalVelocity = jumpForce;
            jumpTimeDelta = JUMP_DELAY;
            jumpTimeout = 0.3f;

            characterAnimator.SetTrigger("JumpTrigger");
        }
    }

    public void Attack()
    {
        characterAnimator.SetTrigger("AttackTrigger");

        overlappedObjects = Physics.OverlapSphere(transform.position, attackRange, characterLayer);
        detectedObjects.Clear();
        for (int i = 0; i < overlappedObjects.Length; i++)
        {
            if (overlappedObjects[i].transform.root == this.transform)
                continue;

            Vector3 direction = overlappedObjects[i].transform.position - transform.position;
            float dot = Vector3.Dot(transform.forward.normalized, direction.normalized);

            if (dot > Mathf.Cos(attackAngle * 0.5f * Mathf.Deg2Rad))
            {
                Debug.Log(overlappedObjects[i].name);
                Vector3 rayStartPos = transform.position + Vector3.up;
                Vector3 rayDirection = overlappedObjects[i].transform.position - transform.position;
                rayDirection.y = 0;

                if (Physics.Raycast(rayStartPos, rayDirection, out RaycastHit hitInfo, attackRange, detectLayer))
                {
                    Debug.Log(overlappedObjects[i].name + " 1");
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Character"))
                    {
                        Debug.Log(overlappedObjects[i].name + " 2");
                        detectedObjects.Add(overlappedObjects[i]);

                        SandBackObject target = hitInfo.transform.GetComponent<SandBackObject>();
                        target.ApplyDamage(attackDamage);
                    }
                }
            }
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
        Vector3 spherePosition = transform.position + (Vector3.down * groundOffset);
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundLayer);

        characterAnimator.SetBool("IsGrounded", isGrounded);
    }
}