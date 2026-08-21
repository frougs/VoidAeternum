using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[RequireComponent(typeof(DamageTypes))]
public class BaseWeapon : ObjectID, IShootable
{
    public string weaponName;
    [HideInInspector] public bool canShoot = true;
    [SerializeField] public float firerate;
    [SerializeField] public float damage;
    public float range;
    public bool hitShake;
    public float shakeAmount;
    public float shakeDuration;
    public DamageTypes.DamageType damageType;
    [HideInInspector] public List<string> damageTypesList = new List<string>();
    [HideInInspector] public CameraShake cam;
    [HideInInspector] public PlayerStats playerStats;
    
    public virtual void Shot()
    {
        UpdateDamageTypes();
        if (canShoot)
        {
            StartCoroutine(ShotDelay());
        }
    }
    public IEnumerator ShotDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(firerate * playerStats.playerFireRateMultiplier);
        canShoot = true;
    }

    public virtual void Update()
    {
        if (cam == null)
        {
            cam = FindObjectOfType<CameraShake>();
        }
        if (playerStats == null)
        {
            playerStats = this.GetComponentInParent<PlayerStats>();
            UpdateDamageTypes();
        }
    }

    public void UpdateDamageTypes()
    {
        foreach (DamageTypes.DamageType type in Enum.GetValues(typeof(DamageTypes.DamageType)))
        {
            if (type == DamageTypes.DamageType.None) continue;

            if (damageType.HasFlag(type) && !damageTypesList.Contains(type.ToString()))
            {
                damageTypesList.Add(type.ToString());
            }
        }
        foreach (string type in playerStats.globalPlayerDamageTypesList)
        {
            if (!damageTypesList.Contains(type))
            {
                damageTypesList.Add(type);
            }
        }
    }
}
