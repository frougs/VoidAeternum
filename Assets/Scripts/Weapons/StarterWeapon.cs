using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterWeapon : BaseWeapon
{
    [Header("Starter Weapon Info")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject projSpawnPoint;
    public override void Shot(){
        if(canShoot){
            UpdateDamageTypes();
            StartCoroutine(ShotDelay());
            LaunchProjectile();
        }
    }
    private void LaunchProjectile(){
        if(projectile != null){
            var launchedProj = Instantiate(projectile, projSpawnPoint.transform.position, Quaternion.identity);
            var projRB = launchedProj.GetComponent<Rigidbody2D>();
            launchedProj.GetComponent<BasicProjectile>().damage = damage * playerStats.playerDamageMulti;
            launchedProj.GetComponent<BasicProjectile>().lifeTime = range * playerStats.playerWeaponRangeMultiplier;
            if (!launchedProj.GetComponent<BasicProjectile>().damageTypes.Contains(damageType.ToString()))
            {
                launchedProj.GetComponent<BasicProjectile>().damageTypes.AddRange(damageTypesList);
            }
            if(projRB != null){
                projRB.AddForce(this.transform.up * (projectileSpeed * playerStats.playerProjectileSpeedMultiplier), ForceMode2D.Impulse);
            }
            if(launchedProj.GetComponent<BasicProjectile>() != null){
                launchedProj.GetComponent<BasicProjectile>().cam = cam;
                launchedProj.GetComponent<BasicProjectile>().hitShake = hitShake;
                launchedProj.GetComponent<BasicProjectile>().shakeAmount = shakeAmount;
                launchedProj.GetComponent<BasicProjectile>().shakeDuration = shakeDuration;
            }
        }
    }
}
