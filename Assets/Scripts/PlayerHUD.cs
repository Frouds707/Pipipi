using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image healthBar; 
    [SerializeField] private TextMeshProUGUI healthText; 
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Player player; 
    [SerializeField] private WeaponManager weaponManager; 

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        if (weaponManager == null)
            weaponManager = FindObjectOfType<WeaponManager>();

        weaponManager.AddWeaponSwitchedListener(UpdateAmmoDisplay);
    }

    private void Update()
    {
        UpdateHealthDisplay(); 
        UpdateAmmoDisplay(); 
    }

    private void UpdateHealthDisplay()
    {
        if (player == null || healthBar == null) return;

        float healthPercentage = player.GetCurrentHealth() / player.GetMaxHealth();

        if (healthPercentage > 0.7f)
            healthBar.color = Color.green;
        else if (healthPercentage > 0.3f)
            healthBar.color = Color.yellow;
        else
            healthBar.color = Color.red;

        
        if (healthText != null)
        {
            healthText.text = $"{Mathf.RoundToInt(player.GetCurrentHealth())} / {Mathf.RoundToInt(player.GetMaxHealth())}";
        }
    }

    private void UpdateAmmoDisplay()
    {
        if (weaponManager == null || ammoText == null) return;

        int currentWeaponIndex = weaponManager.GetCurrentWeaponIndex();
        var weapons = weaponManager.GetWeapons();

        if (weapons[currentWeaponIndex] is Pistol)
        {
            ammoText.text = "∞";
        }
        else if (weapons[currentWeaponIndex] is RocketLauncher rocket)
        {
            ammoText.text = rocket.GetAmmo().ToString();
        }
    }
}