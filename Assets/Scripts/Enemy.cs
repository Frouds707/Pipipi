using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : Character
{
    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private float minDistance = 7f;
    [SerializeField] private Transform target;
    [SerializeField] private WeaponManager weaponManager;
    private PatrolAI patrolAI;
    private bool isShooting = false;
    private Player targetPlayer;

    private Vector3 spawnPoint;
    private bool isDead = false;
    private float respawnTime = 10f;

    protected override void Start()
    {
        base.Start();
        spawnPoint = GetRandomNavMeshPosition();
        if (PlayerManager.instance != null && PlayerManager.instance.player != null)
        {
            target = PlayerManager.instance.player.transform;
            targetPlayer = PlayerManager.instance.player.GetComponent<Player>();
        }

        patrolAI = GetComponent<PatrolAI>();
        weaponManager = GetComponent<WeaponManager>();
        OnDeath += StartRespawn;
    }

    private void Update()
    {
        if (isDead) return;

        if (target == null || targetPlayer == null) return;

        if (!targetPlayer.IsAlive())
        {
            patrolAI.StartPatrolling();
            isShooting = false;
            if (!agent.hasPath && !patrolAI.IsWaiting)
            {
                patrolAI.MoveToNextPatrolPoint();
            }
            return;
        }

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

    protected override void Die()
    {
        base.Die();
    }

    private void StartRespawn()
    {
        isDead = true;
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), respawnTime);
    }

    private void Respawn()
    {
        spawnPoint = GetRandomNavMeshPosition();
        transform.position = spawnPoint;
        currentHealth = maxHealth;
        gameObject.SetActive(true);
        isDead = false;
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * 10f + transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, 10f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
}