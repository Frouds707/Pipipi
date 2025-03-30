using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] private int ammoAmount = 3; 

    protected override bool ApplyEffect(PlayerPickupHandler handler)
    {
        return handler.AddAmmo(ammoAmount);
    }
}
