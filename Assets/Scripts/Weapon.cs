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
        if (data == null) Debug.LogError($"{name}: WeaponData не задан!");
        if (firePoint == null) Debug.LogError($"{name}: Fire Point не задан!");
    }

    public bool CanShoot() => Time.time >= nextFireTime;
    protected void UpdateFireTime() => nextFireTime = Time.time + 1f / data.fireRate;

    public abstract void Shoot(Vector3 direction);
}