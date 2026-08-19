using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : BaseWeapon, IReleasable
{
    [Header("Mining Laser Info")]
    [SerializeField] GameObject laser;
    [SerializeField] GameObject laserOrigin;
    [SerializeField] GameObject hitParticles;
    [SerializeField] LayerMask IgnoreLayer;
    private LaserCollision obj;
    private LineRenderer line;

    private List<string> oldDamageTypes = new List<string>();
    public override void Update()
    {
        base.Update();
        if (obj == null)
        {
            obj = laser.GetComponent<LaserCollision>();
            obj.damage = damage * playerStats.playerDamageMulti;
            obj.firerate = firerate * playerStats.playerFireRateMultiplier;
            obj.damageTypes.Add(damageType.ToString());
            obj.damageTypes.Add(playerStats.playerGlobalDamageTypes.ToString());
            obj.hitParticles = hitParticles;
            obj.range = range * playerStats.playerWeaponRangeMultiplier;
            obj.IgnoreLayer = IgnoreLayer;
            obj.hitShake = hitShake;
            obj.shakeAmount = shakeAmount;
            obj.shakeDuration = shakeDuration;
            line = obj.GetComponent<LineRenderer>();
            obj.cam = cam;
        }
    }
    public override void Shot()
    {
        if (canShoot)
        {
            //RefreshStats();
            obj.ShootLaser(laserOrigin);
            StartCoroutine(ShotDelay());
        }
    }
    public void ShotReleased()
    {
        if (line != null)
        {
            line.positionCount = 0;
        }
    }

    public void RefreshStats()
    {
        obj.damage = damage * playerStats.playerDamageMulti;
        obj.firerate = firerate * playerStats.playerFireRateMultiplier;
        obj.damageTypes.Clear();
        obj.damageTypes.Add(damageType.ToString());
        obj.damageTypes.Add(playerStats.playerGlobalDamageTypes.ToString());
        oldDamageTypes = obj.damageTypes;
        obj.range = range * playerStats.playerWeaponRangeMultiplier;
        obj.IgnoreLayer = IgnoreLayer;
    }
}
