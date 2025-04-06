using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AmmoPickup : Pickup
{
    [SerializeField] private int ammoAmount = 3;

    protected override bool ApplyEffect(PlayerPickupHandler handler)
    {
        WeaponManager weaponManager = handler.GetComponent<WeaponManager>();
        if (weaponManager != null)
        {
            // Ищем RocketLauncher среди всех оружий
            foreach (var weapon in weaponManager.GetWeapons())
            {
                if (weapon is RocketLauncher rocket)
                {
                    rocket.AddAmmo(ammoAmount);
                    return true; // Успешно добавили патроны
                }
            }
        }
        return false; // Не удалось найти RocketLauncher
    }
}
