using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public float fireRate = 1f;
    public float damage = 10f;
    public float projectileSpeed = 20f;
    public GameObject projectilePrefab;
    public float explosionRadius = 0f; 
    public float maxExplosionDamage = 0f; 
}