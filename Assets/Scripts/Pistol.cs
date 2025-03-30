using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot(Vector3 direction)
    {
        if (!CanShoot()) return;

        GameObject projectile = Instantiate(data.projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(direction * data.projectileSpeed, ForceMode.VelocityChange);

        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null) proj.damage = data.damage;

        UpdateFireTime();
    }
}