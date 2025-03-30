using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    bool CanShoot();
    void Shoot(Vector3 direction);
    void Setup(Transform firePoint, GameObject projectilePrefab);
}
