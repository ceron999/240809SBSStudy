using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    #region 공격 데이터 
    public float attackRange = 3f;
    public float attackAngle = 80f;
    public float attackDamage = 10f;
    public LayerMask characterLayer;
    public LayerMask detectLayer;

    public Collider[] overlappedObjects;
    public List<Collider> detectedObjects = new List<Collider>();
    #endregion

    #region 애니메이션
    public Animator characterAnimator;
    public UnityEngine.CharacterController unityCharacterController;

    public bool IsArmed { get; set; } = false;
    public bool IsRunning { get; set; } = false;

    public float moveSpeed = 1.5f;
    public float walkSpeed = 1.5f;
    public float runSpeed = 3f;

    public float speed;
    public float armed;
    public float horizontal;
    public float vertical;
    public float runningBlend;
    #endregion

    public GameObject weaponHolder;

    #region 이동 데이터
    public Vector2 movement;
    public Vector2 movementBlend;
    public float rotation = 0f;

    [Header("점프 세팅")]
    private bool isGrounded = false;
    private float jumpForce = 3f;
    private float verticalVelocity = 0f;
    private float gravity = -9.8f;
    private float jumpTimeDelta = 0f;
    private float jumpTimeout = 0.3f;
    private const float JUMP_DELAY = 1f;
    public float groundCheckDistance = 0.1f;
    public float groundOffset = 0.1f;
    public LayerMask groundLayer;
    #endregion

    #region 스텟 값
    public CharacterStatData characterStat;

    [Header("현재 값")]
    public float currentHP;
    public float currentSP;

    public float CurrentHP => currentHP;
    public float CurrentSP => currentSP;
    public float MaxHP => characterStat.MaxHP;
    public float MaxSP => characterStat.MaxSP;
    #endregion

    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        unityCharacterController = GetComponent<UnityEngine.CharacterController>();

        currentHP = characterStat.MaxHP;
        currentSP = characterStat.MaxSP;
    }

    private void Start()
    {

    }

    private void Update()
    {
        // 1. 지형 확인
        CheckGround();
        FreeFall();

        // 이동(걷기, 뛰기)
        Vector3 moveVec = new Vector3(horizontal, verticalVelocity, vertical);

        if (IsRunning)
        {
            currentSP -= (characterStat.RunStaminaCost * Time.deltaTime);
            currentSP = Mathf.Clamp(currentSP, 0, characterStat.MaxSP);
        }
        else
        {
            currentSP += (characterStat.StaminaRecoverySpeed * Time.deltaTime);
            currentSP = Mathf.Clamp(currentSP, 0, characterStat.MaxSP);
        }

        runningBlend = Mathf.Lerp(runningBlend, IsRunning && currentSP > 0 ? 1f : 0f, Time.deltaTime * 10f);

        // Movement Blend : 를 이용하는 이유는 애니메이션에 적용할 parameter 값을 갑자기 튀지 않도록
        // 하기 위해서 중간(보간)값을 구해서 적용을 했음
        //movementBlend = Vector2.Lerp(movementBlend, movement, Time.deltaTime * 10f);

        // 3. 애니메이션 값 설정
        armed = Mathf.Lerp(armed, IsArmed ? 1f : 0f, Time.deltaTime * 10f);
        runningBlend = Mathf.Lerp(runningBlend, IsRunning ? 1f : 0f, Time.deltaTime * 10f);

        characterAnimator.SetFloat("Speed", speed);
        characterAnimator.SetFloat("Armed", armed);
        characterAnimator.SetFloat("Horizontal", horizontal);
        characterAnimator.SetFloat("Vertical", vertical);
        characterAnimator.SetFloat("RunningBlend", runningBlend);
    }

    #region 이동 및 회전, 점프 함수
    public void Move(Vector2 input, float yAxisAngle)
    {
        horizontal = input.x;
        vertical = input.y;

        speed = input.magnitude > 0 ? 1f : 0f;

        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        moveSpeed = IsRunning ? runSpeed : walkSpeed;
        unityCharacterController.Move(movement * moveSpeed * Time.deltaTime);
    }


    public void Rotate(float angle)
    {
        rotation += angle;
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void SetRunning(bool isRun)
    {
        IsRunning = isRun;
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
    #endregion

    // TPS 전용 함수
    public void SetArmed(bool isArmed)
    {
        IsArmed = isArmed;
        weaponHolder.SetActive(isArmed);
    }

    public void Shoot()
    {

    }

    #region 지형 확인 함수
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
    #endregion
}