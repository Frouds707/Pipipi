using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    private bool isShooting = false;
    public float LookRadius;
    private PatrolAI patrolAI;
    private EnemyShooter shooter;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        patrolAI = GetComponent<PatrolAI>();
        shooter = GetComponent<EnemyShooter>();
        shooter.target = target;

    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= LookRadius && CanSeePlayer())
        {
            patrolAI.StopPatrolling();
            LookTarget(); 

            if (!isShooting && distance > shooter.minDistance)
            {
                agent.SetDestination(target.position); 
                
            }
            else
            {
                agent.SetDestination(transform.position); 
                shooter.EngageTarget(); 
                isShooting = true;
                
            }
        }
        else
        {
            patrolAI.StartPatrolling();
        }
        
    }

    private void LookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }

    private bool CanSeePlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, LookRadius))
        {
            if (hit.transform == target)
            {
                return true;
            }
            return false;
        }
        return true;
    }
}
