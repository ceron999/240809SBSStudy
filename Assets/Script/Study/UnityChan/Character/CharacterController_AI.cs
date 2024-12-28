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

        if(aiState == AIState.Battle && target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, patrolWayPoints[currentWayPointIndex].position);
        }

        if(aiState == AIState.Peaceful && patrolWayPoints.Count >0)
        {
            Gizmos.color= Color.blue;
            Gizmos.DrawLine(transform.position, patrolWayPoints[currentWayPointIndex].position);
        }
    }
    public CharacterBase linkedCharacter;
    public UnityEngine.AI.NavMeshAgent navAgent;

    public AIState aiState = AIState.Peaceful;
    public float detectionRadius = 10f;
    public LayerMask detectionLayers;

    public List<Transform> patrolWayPoints = new List<Transform>();
    public int currentWayPointIndex = 0;

    public CharacterBase target = null;

    private void Awake()
    {
        linkedCharacter = GetComponent<CharacterBase>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            OnArriveDestination(); ;
        }

        if(navAgent.hasPath)
        {
            Vector3 moveDistance = (navAgent.steeringTarget - transform.position).normalized;
            Vector2 input = new Vector2(moveDistance.x, moveDistance.y);

            linkedCharacter.Move(input, 0);
        }

        UpdateDetecting();
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
                    target = character;
                    aiState = AIState.Battle;
                    break;
                }
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        navAgent.SetDestination(destination);   
    }
    public void OnArriveDestination()
    {
        MoveToNextWayPoint();
    }

    public void MoveToNextWayPoint()
    {
        if (patrolWayPoints.Count <= 0)
            return;

        currentWayPointIndex++;

        if(currentWayPointIndex > patrolWayPoints.Count)
            currentWayPointIndex = 0;

        SetDestination(patrolWayPoints[currentWayPointIndex].position);
    }
}