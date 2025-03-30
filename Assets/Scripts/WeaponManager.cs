using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private IWeapon[] weapons;
    [SerializeField] private Transform firePoint;
    [SerializeField] private UnityEvent onWeaponSwitched;
    private int currentWeaponIndex = 0;

    private void Awake()
    {
        weapons = GetComponents<IWeapon>();
        if (weapons.Length == 0) Debug.LogError("Нет оружий на объекте!");
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Setup(firePoint, GetProjectilePrefab(i));
        }
    }

    public void Shoot(Vector3 direction)
    {
        if (weapons[currentWeaponIndex] != null && weapons[currentWeaponIndex].CanShoot())
        {
            weapons[currentWeaponIndex].Shoot(direction);
        }
    }

    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length)
        {
            currentWeaponIndex = index;
            onWeaponSwitched?.Invoke();
            Debug.Log($"Выбрано оружие: {weapons[currentWeaponIndex].GetType().Name}");
        }
    }

    public int GetCurrentAmmo()
    {
        if (weapons[currentWeaponIndex] is RocketLauncher rocket)
        {
            return rocket.GetAmmo();
        }
        return -1; 
    }

    public void AddAmmo(int amount)
    {
        if (weapons[currentWeaponIndex] is RocketLauncher rocket)
        {
            rocket.AddAmmo(amount);
        }
    }


    private GameObject GetProjectilePrefab(int index)
    {
        if (weapons[index] is Pistol) return Resources.Load<GameObject>("BulletPrefab");
        if (weapons[index] is RocketLauncher) return Resources.Load<GameObject>("RocketPrefab");
        return null;
    }
}

