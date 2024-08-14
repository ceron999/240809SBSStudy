using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class PlayerNetwork : NetworkBehaviour
{
    Transform playerTransform;
    Rigidbody playerRigid;

    #region Move Variable
    [Header("이동 변수")]
    public float moveSpeed = 5f;
    public Vector3 moveDir = Vector3.forward;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = this.transform;
        playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //IsOwner : 객체가 로컬 플레이어 소유인지 로컬 플레이어인지 구분
        if (!IsOwner) return;

        Move();
    }

    private void Move()
    {
        Vector3 targetVector = Vector3.zero;

        //앞
        if(Input.GetKey(KeyCode.W))
        {
            targetVector = transform.TransformDirection(Vector3.forward * moveSpeed);
            moveDir = targetVector.normalized;

            if (playerRigid.velocity.magnitude < moveSpeed)
            { 
                playerRigid.AddForce(moveDir * moveSpeed);
            }
            else
                playerRigid.velocity = moveDir * moveSpeed;
        }
        //왼
        if (Input.GetKey(KeyCode.A))
        {
            targetVector = transform.TransformDirection(Vector3.left * moveSpeed);
            moveDir = targetVector.normalized;

            if (playerRigid.velocity.magnitude < moveSpeed)
            {
                playerRigid.AddForce(moveDir * moveSpeed);
            }
            else
                playerRigid.velocity = moveDir * moveSpeed;
        }
        //뒤
        if (Input.GetKey(KeyCode.S))
        {
            targetVector = transform.TransformDirection(Vector3.back * moveSpeed);
            moveDir = targetVector.normalized;

            if (playerRigid.velocity.magnitude < moveSpeed)
            {
                playerRigid.AddForce(moveDir * moveSpeed);
            }
            else
                playerRigid.velocity = moveDir * moveSpeed;
        }
        //오
        if (Input.GetKey(KeyCode.D))
        {
            targetVector = transform.TransformDirection(Vector3.right * moveSpeed);
            moveDir = targetVector.normalized;

            if (playerRigid.velocity.magnitude < moveSpeed)
            {
                playerRigid.AddForce(moveDir * moveSpeed);
            }
            else
                playerRigid.velocity = moveDir * moveSpeed;
        }
        
        //아무것도
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            playerRigid.velocity = Vector3.zero;
        }
    }
}
