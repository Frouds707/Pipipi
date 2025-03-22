using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    public float minDistance = 5f;      // Минимальная дистанция для стрельбы

    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint;         // Точка, откуда стреляет бот
    public float fireRate = 1f;         // Частота стрельбы (выстрелов в секунду)
    private float nextFireTime = 0f;    // Время следующего выстрела

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void EngageTarget()
    {
        LookTarget(); // Поворачиваемся к игроку во время стрельбы

        // Стрельба
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
           
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
    //private NavMeshAgent agent;
    //public Transform target;
    //public float minDistance = 5f;


    //public GameObject projectilePrefab;
    //public Transform firePoint;
    //public float fireRate = 1f;
    //private float nextFireTime = 0f;
    //private void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //}


    //public void EngageTarget()
    //{
    //    float distance = Vector3.Distance(target.position, transform.position);


    //    agent.SetDestination(transform.position); 
    //    LookTarget();


    //    if (Time.time >= nextFireTime)
    //    {
    //        Shoot();
    //        nextFireTime = Time.time + 1f / fireRate;
    //    }
    //}

    //private void Shoot()
    //{

    //    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    //    Vector3 direction = (target.position - firePoint.position).normalized;
    //    projectile.GetComponent<Rigidbody>().velocity = direction * 20f; 
    //}

    //private void LookTarget()
    //{
    //    Vector3 direction = (target.position - transform.position).normalized;
    //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, minDistance); 
    //}
}
