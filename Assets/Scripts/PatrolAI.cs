using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class PatrolAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float patrolRadius = 20f;
    private Vector3 patrolPoint;
    private bool isPatrolPointSet = false;
    private bool isWaiting = false;
    public float waitTime = 1f;
    private float waitTimer = 0f;

    public bool IsWaiting => isWaiting;

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
        if (!isPatrolPointSet) return;

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                SetNewPatrolPoint();
            }
            return;
        }

        float distanceToPoint = Vector3.Distance(transform.position, patrolPoint);

        if (agent.hasPath && !agent.pathPending && distanceToPoint <= 0.5f)
        {
            isWaiting = true;
            agent.ResetPath();
        }
    }

    private void SetNewPatrolPoint()
    {
        Vector3 newPoint;
        bool validPointFound = false;
        int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
            {
                newPoint = hit.position;

                NavMeshPath path = new NavMeshPath();
                bool hasValidPath = agent.CalculatePath(newPoint, path) && path.status == NavMeshPathStatus.PathComplete;
                bool hasLineOfSight = HasClearPath(transform.position, newPoint);

                if (hasValidPath && hasLineOfSight)
                {
                    patrolPoint = newPoint;
                    validPointFound = true;
                    agent.SetDestination(patrolPoint);
                    break;
                }
            }
        }

        if (!validPointFound)
        {
            isPatrolPointSet = false;
            return;
        }

        isPatrolPointSet = true;
    }

    private bool HasClearPath(Vector3 start, Vector3 end)
    {
        if (NavMesh.Raycast(start, end, out NavMeshHit hit, NavMesh.AllAreas))
        {
            Debug.DrawLine(start, hit.position, Color.red, 1f);
            return false;
        }

        Debug.DrawLine(start, end, Color.green, 1f);
        return true;
    }

    public void StopPatrolling()
    {
        enabled = false;
        isPatrolPointSet = false;
        isWaiting = false;
        agent.ResetPath();
    }

    public void StartPatrolling()
    {
        enabled = true;
        if (!isPatrolPointSet)
        {
            SetNewPatrolPoint();
        }
    }

    public void MoveToNextPatrolPoint()
    {
        if (!isWaiting)
        {
            SetNewPatrolPoint();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
        if (isPatrolPointSet)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, patrolPoint);
        }
    }
}