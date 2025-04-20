using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealthPickup : Pickup
{
    [SerializeField] private float healthAmount = 20f;

    protected override bool ApplyEffect(PlayerPickupHandler handler)
    {
        return handler.AddHealth(healthAmount);
    }
}