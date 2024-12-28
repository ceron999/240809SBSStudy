using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterBase : MonoBehaviour, IDamage
{
    public Animator characterAnimator;
    public UnityEngine.CharacterController unityCharacterController;
    public RigBuilder rigBuilder;
    public Rig aimRig;
    public Rig leftHandRig;
    public Transform cameraPivot;

    public CharacterStatData maxStat;
    public CharacterStatData curStat;

    public bool IsArmed { get; set; } = false;
    public bool IsRun { get; set; } = false;
    public Vector3 AimingPoint
    {
        get => aimingPointTransform.position;
        set => aimingPointTransform.position = value;
    }

    // move 변수
    public float speed;
    public float armed;
    public float horizontal;
    public float vertical;
    public float runningBlend;

    public float moveSpeed = 3f;
    public float targetRotation = 0f;
    public float rotationSpeed = 0.1f;

    // 지형 확인 변수
    private float verticalVelocity = 0;
    private bool isGrounded = false;
    public float groundOffset = 0.1f;
    public float checkRadius = 0.1f;
    public LayerMask groundLayers;

    public Transform weaponSocket;
    public GameObject weaponHolder;
    public WeaponBase currentWeapon;
    public Transform aimingPointTransform;

    private bool isShooting = false;
    private bool isReloading = false;
    private bool isEquipped = false;

    private void Awake()
    {
        rigBuilder = GetComponent<RigBuilder>();
        characterAnimator = GetComponent<Animator>();
        unityCharacterController = GetComponent<UnityEngine.CharacterController>();

        aimRig.weight = 0f;
        leftHandRig.weight = 0f;

        curStat = ScriptableObject.CreateInstance<CharacterStatData>();
        curStat.HP = maxStat.HP;
        curStat.SP = maxStat.SP;
        curStat.WalkSpeed = maxStat.WalkSpeed;
        curStat.RunSpeed = maxStat.RunSpeed;
        curStat.RunStaminaCost = maxStat.RunStaminaCost;
        curStat.StaminaRecoverySpeed = maxStat.StaminaRecoverySpeed;
    }

    private void Update()
    {
        CheckGround();
        FreeFall();

        if (isShooting)
        {
            bool isFireSuccess = currentWeapon.Fire();
            if (false == isFireSuccess)
            {
                if (currentWeapon.RemainAmmo <= 0 && false == isReloading)
                {
                    Reload();
                }
            }
        }

        armed = Mathf.Lerp(armed, IsArmed ? 1f : 0f, Time.deltaTime * 10f);
        runningBlend = Mathf.Lerp(runningBlend, IsRun ? 1f : 0f, Time.deltaTime * 10f);

        characterAnimator.SetFloat("Speed", speed);
        characterAnimator.SetFloat("Armed", armed);
        characterAnimator.SetFloat("Horizontal", horizontal);
        characterAnimator.SetFloat("Vertical", vertical);
        characterAnimator.SetFloat("RunningBlend", runningBlend);

        aimRig.weight = isEquipped ? 1f : 0f;
        leftHandRig.weight = isEquipped ? 1f : 0f;
    }


    public void Shoot(bool isShoot)
    {
        isShooting = isShoot;
    }

    public void SetArmed(bool isArmed)
    {
        IsArmed = isArmed;
        if (IsArmed)
        {
            characterAnimator.SetTrigger("Equip Trigger");
        }
        else
        {
            characterAnimator.SetTrigger("Holster Trigger");
        }
    }

    public void SetEquipState(int equipState)
    {
        bool isEquip = equipState == 1;
        if (isEquip)
        {
            currentWeapon.transform.SetParent(weaponHolder.transform);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            currentWeapon.transform.SetParent(weaponSocket.transform);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }

        isEquipped = isEquip;
    }

    public void CheckGround()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * groundOffset), Vector3.down);
        isGrounded = Physics.SphereCast(ray, checkRadius, 0.1f, groundLayers);
    }

    public void FreeFall()
    {
        if (!isGrounded)
        {
            verticalVelocity = Mathf.Lerp(verticalVelocity, -9.8f, Time.deltaTime);
        }
        else
        {
            verticalVelocity = 0;
        }
    }
    public void Move(Vector2 input, float yAxisAngle)
    {
        horizontal = input.x;
        vertical = input.y;
        speed = input.magnitude > 0f ? 1f : 0f;

        Vector3 movement = Vector3.zero;
        if (IsArmed)
        {
            movement = transform.forward * vertical + transform.right * horizontal;
        }
        else
        {
            if (input.magnitude > 0f)
            {
                targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + yAxisAngle;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationSpeed, 0.1f);
                transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            }

            movement = transform.forward * speed;
        }

        movement.y = verticalVelocity;
        unityCharacterController.Move(movement * Time.deltaTime * moveSpeed);
    }

    public void Rotate(float angle)
    {
        float rotation = transform.rotation.eulerAngles.y + angle;
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void Reload()
    {
        isReloading = true;
        characterAnimator.SetTrigger("Reload Trigger");
        leftHandRig.weight = 0f;
    }

    public void ReloadComplete()
    {
        currentWeapon.Reload();
        leftHandRig.weight = 1f;
        isReloading = false;

        rigBuilder.Build();
    }

    public void ApplyDamage(float damage)
    {
        curStat.HP -= damage;
        if(curStat.HP <= 0)
        {
            // 사망
        }
    }
}
