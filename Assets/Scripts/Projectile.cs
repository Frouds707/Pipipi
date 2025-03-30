using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    private bool hasCollided = false;

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

        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            character.TakeDamage(damage);
            Debug.Log($"{other.name} получил урон: {damage}");
            Destroy(gameObject);
        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") && !other.CompareTag("Player"))
        {
            Destroy(gameObject); 
        }
    }
}