using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class RocketLauncher : Weapon
{
    [SerializeField] private int maxAmmo = 20; 
    private int currentAmmo = 0;

    private void Awake()
    {
        currentAmmo = 0; 
    }

    public override void Shoot(Vector3 direction)
    {
        if (!CanShoot() || currentAmmo <= 0) return; 

        GameObject projectile = Instantiate(data.projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(direction * data.projectileSpeed, ForceMode.VelocityChange);

        Rocket rocket = projectile.GetComponent<Rocket>();
        if (rocket != null) rocket.SetExplosion(data.maxExplosionDamage, data.explosionRadius);

        currentAmmo--; 
        Debug.Log($"Осталось ракет: {currentAmmo}");
        UpdateFireTime();
    }

    
    public int GetAmmo()
    {
        return currentAmmo;
    }

    public void AddAmmo(int amount)
    {
        if (currentAmmo < maxAmmo)
        {
            currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
            Debug.Log($"Добавлено патронов: {amount}, теперь: {currentAmmo}");
        }
        else
        {
            Debug.Log("Патроны для ракетницы максимум!");
        }
    }
}