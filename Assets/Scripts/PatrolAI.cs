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

   
    public bool IsWaiting
    {
        get { return isWaiting; }
    }

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
                Debug.Log($"PatrolAI: Ожидание на точке {patrolPoint} завершено");
            }
            return;
        }

        float distanceToPoint = Vector3.Distance(transform.position, patrolPoint);
        Debug.Log($"PatrolAI: Физическое расстояние до точки = {distanceToPoint}, hasPath = {agent.hasPath}, pathPending = {agent.pathPending}, remainingDistance = {agent.remainingDistance}");

        if (agent.hasPath && !agent.pathPending && distanceToPoint <= 0.5f)
        {
            isWaiting = true;
            agent.ResetPath();
            Debug.Log($"PatrolAI: Дошел до точки {patrolPoint}, ждем {waitTime} секунд");
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

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
            {
                newPoint = hit.position;

                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(newPoint, path) && path.status == NavMeshPathStatus.PathComplete)
                {
                    patrolPoint = newPoint;
                    validPointFound = true;
                    agent.SetDestination(patrolPoint);
                    Debug.Log($"PatrolAI: Новая точка патрулирования: {patrolPoint}");
                    break;
                }
                else
                {
                    Debug.LogWarning($"PatrolAI: Точка {newPoint} недостижима, ищем другую...");
                }
            }
            else
            {
                Debug.LogWarning($"PatrolAI: Точка {randomDirection} не на NavMesh, ищем другую...");
            }
        }

        if (!validPointFound)
        {
            Debug.LogWarning("PatrolAI: Не удалось найти достижимую точку для патрулирования!");
            isPatrolPointSet = false;
            return;
        }

        isPatrolPointSet = true;
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