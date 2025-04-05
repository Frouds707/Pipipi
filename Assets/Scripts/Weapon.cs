using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected WeaponData data;
    protected Transform firePoint;
    protected float nextFireTime = 0f;

    public virtual void Setup(Transform point, GameObject prefab)
    {
        firePoint = point;
    }

    public bool CanShoot() => Time.time >= nextFireTime;
    protected void UpdateFireTime() => nextFireTime = Time.time + 1f / data.fireRate;

    public abstract void Shoot(Vector3 direction);
}