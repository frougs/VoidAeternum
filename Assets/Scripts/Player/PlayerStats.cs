using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public float playerMoveSpeed;
    public float playerThrust;
    public float maxFuel;
    public float playerDamageMulti;
    public float playerDamageReduction;
    public int playerLives;
    public float playerFireRateMultiplier;
    public float playerProjectileSpeedMultiplier;
    public float playerWeaponRangeMultiplier;
    public float expMultiplier;
    public DamageTypes.DamageType playerGlobalDamageTypes;
    public List<string> globalPlayerDamageTypesList = new List<string>();
    public UnityEvent<float, float, float> updatedHealth;
    public UnityEvent<float, float> updatedFuel;

    public void Start()
    {
        UpdatePlayerGlobalDamageTypes();
        FullHeal();

    }

    public void UpdatePlayerGlobalDamageTypes()
    {
        foreach (DamageTypes.DamageType type in Enum.GetValues(typeof(DamageTypes.DamageType)))
        {
            if (type == DamageTypes.DamageType.None) continue;

            if (playerGlobalDamageTypes.HasFlag(type))
            {
                globalPlayerDamageTypesList.Add(type.ToString());
            }
        }
    }

    public void TakeDamage(float damage)
    {
        var savedCurHealth = playerCurrentHealth;
        var adjustedDamage = damage - playerDamageReduction;
        playerCurrentHealth -= adjustedDamage;
        updatedHealth.Invoke(savedCurHealth, playerCurrentHealth, playerMaxHealth);
    } 

    public void FullHeal()
    {
        playerCurrentHealth = playerMaxHealth;
        updatedHealth.Invoke(playerCurrentHealth, playerMaxHealth, playerMaxHealth);
    }
    public void UpdateFuel(float currentFuel)
    {
        updatedFuel.Invoke(currentFuel, maxFuel);
    }
}
