using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AmmoPickup : Pickup
{
    [SerializeField] private int ammoAmount = 3;

    protected override bool ApplyEffect(PlayerPickupHandler handler)
    {
        bool success = handler.AddAmmo(ammoAmount);
        if (success)
        {
            
            WeaponManager weaponManager = handler.GetComponent<WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.SwitchWeapon(weaponManager.GetCurrentWeaponIndex());  
            }
        }
        return success;
    }
}
