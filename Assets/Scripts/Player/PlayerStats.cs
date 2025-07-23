using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public float playerMaxHealth;
    public float playerMoveSpeed;
    public float playerThrust;
    public float playerDamageMulti;
    public float playerDamageReduction;
    public int playerLives;
    public float playerFireRateMultiplier;
    public float playerProjectileSpeedMultiplier;
    public float playerWeaponRangeMultiplier;
    public DamageTypes.DamageType playerGlobalDamageTypes;
    public List<string> globalPlayerDamageTypesList = new List<string>();

    public void Start()
    {
        UpdatePlayerGlobalDamageTypes();

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
}
