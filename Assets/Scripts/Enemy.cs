using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private float minDistance = 7f;
    [SerializeField] private Transform target;
    [SerializeField] private WeaponManager weaponManager; 
    private PatrolAI patrolAI;
    private bool isShooting = false;

    protected override void Start()
    {
        base.Start();
        if (PlayerManager.instance != null && PlayerManager.instance.player != null)
        {
            target = PlayerManager.instance.player.transform;
        }
        else
        {
            Debug.LogError("PlayerManager или player не найдены! Укажи цель вручную.");
        }

        patrolAI = GetComponent<PatrolAI>();
        if (patrolAI == null) Debug.LogError("PatrolAI не найден!");

        weaponManager = GetComponent<WeaponManager>();
        if (weaponManager == null) Debug.LogError("WeaponManager не найден на враге!");
    }

   
    private void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius && CanSeePlayer())
        {
            patrolAI.StopPatrolling();
            LookTarget(target);

            if (!isShooting && distance > minDistance)
            {
                agent.SetDestination(target.position);
            }
            else
            {
                agent.SetDestination(transform.position);
                if (weaponManager != null)
                {
                    Vector3 direction = (target.position - transform.position).normalized;
                    weaponManager.Shoot(direction);
                }
                isShooting = true;
            }
        }
        else
        {
            patrolAI.StartPatrolling();
            isShooting = false;

            
            if (!agent.hasPath && !patrolAI.IsWaiting)
            {
                patrolAI.MoveToNextPatrolPoint();
            }
        }
    }

    private bool CanSeePlayer()
    {
        if (target == null) return false;

        Vector3 direction = (target.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, lookRadius))
        {
            return hit.transform == target;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}