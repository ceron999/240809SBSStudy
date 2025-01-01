using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
    Peaceful,
    Battle,
}

public class CharacterController_AI : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (aiState == AIState.Battle && target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }

        if (aiState == AIState.Peaceful && patrolWaypoints.Count > 0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, patrolWaypoints[currentWaypointIndex].position);
        }
    }

    public CharacterBase linkedCharacter;
    public UnityEngine.AI.NavMeshAgent navAgent;

    public AIState aiState = AIState.Peaceful;
    public float detectionRadius = 10f;
    public LayerMask detectionLayers;

    public List<Transform> patrolWaypoints = new List<Transform>();
    public int currentWaypointIndex = 0;

    public CharacterBase target = null;

    private void Awake()
    {
        linkedCharacter = GetComponent<CharacterBase>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navAgent.updateRotation = false;
    }

    private void Start()
    {
        navAgent.speed = linkedCharacter.moveSpeed;
        linkedCharacter.SetArmed(true);
        linkedCharacter.IsNPC = true;
        SetAiState(AIState.Peaceful);
    }

    private void Update()
    {
        if (aiState == AIState.Peaceful)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                OnArrivedDestination();
            }

            if (navAgent.hasPath)
            {
                Vector3 moveDirection = (navAgent.steeringTarget - transform.position).normalized;
                Vector3 localDirection = linkedCharacter.transform.InverseTransformDirection(moveDirection);
                Vector2 input = new Vector2(localDirection.x, localDirection.z);

                linkedCharacter.Move(input, 0);
                linkedCharacter.transform.forward = moveDirection;
            }

            UpdateDetecting();
        }
        else if (aiState == AIState.Battle)
        {
            UpdateCombat();
        }
    }

    private void UpdateCombat()
    {
        if (aiState != AIState.Battle || target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.transform.position);
        float weaponRange = 7f;
        if (distance > weaponRange)
        {
            ChaseTarget();
            linkedCharacter.Shoot(false);

            if(navAgent.hasPath)
            {
                Vector3 moveDirection = (navAgent.steeringTarget - transform.position).normalized;
                Vector3 localDirerction = linkedCharacter.transform.InverseTransformDirection(moveDirection);
                Vector2 input = new Vector2(localDirerction.x, localDirerction.z);

                linkedCharacter.Move(input, 0);
                linkedCharacter.transform.forward = moveDirection;

            }
        }
        else
        {
            if(target.IsAlive)
            {
                // TODO : Attack
                Transform targetChestTransform = target.GetBoneTransform(HumanBodyBones.Chest);
                linkedCharacter.AimingPoint = targetChestTransform.position;
                linkedCharacter.transform.forward = (target.transform.position - transform.position).normalized;
                linkedCharacter.Move(Vector2.zero, 0);
                linkedCharacter.Shoot(true);
            }
            else
            {
                target = null;
                linkedCharacter.Shoot(false);
                SetAiState(AIState.Peaceful);
            }

        }
    }

    private void UpdateDetecting()
    {
        if (aiState != AIState.Peaceful)
            return;

        Collider[] detectionColliders = Physics.OverlapSphere(
            transform.position,
            detectionRadius,
            detectionLayers,
            QueryTriggerInteraction.Ignore);

        if (detectionColliders.Length <= 0)
            return;

        for (int i = 0; i < detectionColliders.Length; i++)
        {
            if (detectionColliders[i].transform.root.TryGetComponent(out CharacterBase character))
            {
                if (character.gameObject.CompareTag("Player"))
                {
                    if(character.IsAlive)
                    {
                        target = character;
                        SetAiState(AIState.Battle);
                        break;
                    }
                }
            }
        }
    }

    public void SetAiState(AIState state)
    {
        aiState = state;
        linkedCharacter.IsAimingActive = aiState == AIState.Battle;
        navAgent.SetDestination(transform.position);
    }

    public void SetDestination(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }

    public void OnArrivedDestination()
    {
        MoveToNextWaypoint();
    }

    public void MoveToNextWaypoint()
    {
        if (patrolWaypoints.Count <= 0)
            return;

        currentWaypointIndex++;
        if (currentWaypointIndex >= patrolWaypoints.Count)
            currentWaypointIndex = 0;

        SetDestination(patrolWaypoints[currentWaypointIndex].position);
    }

    public void ChaseTarget()
    {
        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.transform.position);
        float weaponRange = 7f;
        if (distance > weaponRange)
        {
            SetDestination(target.transform.position);
        }
        else
        {
            SetDestination(transform.position);
        }
    }
}