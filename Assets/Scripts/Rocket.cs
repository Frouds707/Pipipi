using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float maxDamage;
    private float explosionRadius;
    private bool hasCollided = false;

    public void SetExplosion(float damage, float radius)
    {
        maxDamage = damage;
        explosionRadius = radius;
    }

    private void Start()
    {
        Invoke("EnableCollision", 0.05f);

    }

    private void EnableCollision()
    {
        hasCollided = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided) return;
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hitColliders)
        {
            Character character = hit.GetComponent<Character>();
            if (character != null)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                float damage = CalculateDamage(distance);
                character.TakeDamage(damage);
            }
        }
    }

    private float CalculateDamage(float distance)
    {
        float damageFalloff = 1 - (distance / explosionRadius);
        return Mathf.Max(0, maxDamage * damageFalloff);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}