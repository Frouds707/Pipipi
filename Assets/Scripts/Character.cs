using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        weapon = GetComponent<IWeapon>();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public void AddHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"{name}: Здоровье увеличено до {currentHealth}");
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        Debug.Log($"{name}: Получен урон {damage}, текущее здоровье: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
    }

    protected void LookTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected NavMeshAgent agent;
    [SerializeField] protected IWeapon weapon;
}