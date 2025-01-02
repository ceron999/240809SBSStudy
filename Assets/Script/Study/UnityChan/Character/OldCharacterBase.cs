using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace OldScript
{
    public class OldCharacterBase : MonoBehaviour
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
        #endregion

        #region 이동 데이터
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
        //public float MaxHP => characterStat.HP;
        //public float MaxSP => characterStat.SP;
        #endregion

        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();

            //currentHP = characterStat.HP;
            //currentSP = characterStat.SP;
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
            Vector3 moveVec = new Vector3(movementBlend.x, verticalVelocity, movementBlend.y);

            if (isRunning)
            {
                //currentSP -= (characterStat.RunStaminaCost * Time.deltaTime);
                //currentSP = Mathf.Clamp(currentSP, 0, characterStat.SP);
            }
            else
            {
                //currentSP += (characterStat.StaminaRecoverySpeed * Time.deltaTime);
                //currentSP = Mathf.Clamp(currentSP, 0, characterStat.SP);
            }
            //float targetSpeed = isRunning && currentSP > 0 ? characterStat.RunSpeed : characterStat.WalkSpeed;

            Vector3 cameraForward = Camera.main.transform.forward.normalized;
            cameraForward.y = 0;
            Vector3 cameraRight = Camera.main.transform.right.normalized;
            cameraRight.y = 0;
            Vector3 resultMovement = cameraForward * moveVec.z + cameraRight * moveVec.x;
            resultMovement.y = verticalVelocity;

            //unityCharacterController.Move(targetSpeed * resultMovement * Time.deltaTime);

            runningBlend = Mathf.Lerp(runningBlend, isRunning && currentSP > 0 ? 1f : 0f, Time.deltaTime * 10f);

            // Movement Blend : 를 이용하는 이유는 애니메이션에 적용할 parameter 값을 갑자기 튀지 않도록
            // 하기 위해서 중간(보간)값을 구해서 적용을 했음
            movementBlend = Vector2.Lerp(movementBlend, movement, Time.deltaTime * 10f);

            // 3. 애니메이션 값 설정
            characterAnimator.SetFloat("Magnitude", movementBlend.magnitude);
            characterAnimator.SetFloat("Horizontal", movementBlend.x);
            characterAnimator.SetFloat("Vertical", movementBlend.y);
            characterAnimator.SetFloat("RunningBlend", runningBlend);
        }

        #region 이동 및 회전, 점프 함수
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
        #endregion

        // 캐릭터가 근접 공격을 하는 이전 함수
        public void OldAttack()
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

                            if (hitInfo.transform.TryGetComponent(out IDamage target))
                            {
                                target.ApplyDamage(attackDamage);
                            }
                        }
                    }
                }
            }
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

        // TPS 전용 함수
        public void Shoot()
        {

        }
    }
}