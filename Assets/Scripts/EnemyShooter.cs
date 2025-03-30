using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    public float minDistance = 5f;      

    public GameObject projectilePrefab; 
    public Transform firePoint;         
    public float fireRate = 1f;         
    private float nextFireTime = 0f;   

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (projectilePrefab == null) Debug.LogError("Projectile Prefab не задан!");
        if (firePoint == null) Debug.LogError("Fire Point не задан!");
    }

    public void EngageTarget()
    {
        LookTarget(); 

       
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
            Debug.Log("Выстрел!");
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Vector3 direction = (target.position - firePoint.position).normalized;
        projectile.GetComponent<Rigidbody>().velocity = direction * 20f;
    }

    private void LookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}