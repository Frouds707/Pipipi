using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float patrolRadius = 20f;       
    private Vector3 patrolPoint;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewPatrolPoint();
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNewPatrolPoint();
        }
        agent.SetDestination(patrolPoint);
    }

    private void SetNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
        }
    }

    public void StopPatrolling()
    {
        enabled = false;
    }

    public void StartPatrolling()
    {
        enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, patrolPoint);
    }
}
