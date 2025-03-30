using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupHandler : MonoBehaviour
{
    private WeaponManager weaponManager;
    private Character character;

    private void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        character = GetComponent<Character>();
        if (weaponManager == null) Debug.LogError("WeaponManager не найден на игроке!");
        if (character == null) Debug.LogError("Character не найден на игроке!");
    }

    public bool AddAmmo(int amount)
    {
        if (weaponManager != null)
        {
            weaponManager.AddAmmo(amount);
            return true;
        }
        return false;
    }

    public bool AddHealth(float amount)
    {
        if (character != null)
        {
            character.AddHealth(amount); 
            return true;
        }
        return false;
    }
}