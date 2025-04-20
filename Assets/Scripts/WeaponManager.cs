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
    private bool isWeaponEnabled = true;

    private void Awake()
    {
        weapons = GetComponents<IWeapon>();
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Setup(firePoint, GetProjectilePrefab(i));
        }
    }

    public void Shoot(Vector3 direction)
    {
        if (!isWeaponEnabled) return;

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

    public int GetCurrentWeaponIndex()
    {
        return currentWeaponIndex;
    }

    public void EnableWeapon()
    {
        isWeaponEnabled = true;
    }

    public void DisableWeapon()
    {
        isWeaponEnabled = false;
    }

    public IWeapon[] GetWeapons()
    {
        return weapons;
    }

    public void AddWeaponSwitchedListener(UnityAction listener)
    {
        onWeaponSwitched.AddListener(listener);
    }
}