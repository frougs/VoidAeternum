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
            StartCoroutine(ShotDelay());
            LaunchProjectile();
        }
    }
    private void LaunchProjectile(){
        if(projectile != null){
            var launchedProj = Instantiate(projectile, projSpawnPoint.transform.position, Quaternion.identity);
            var projRB = launchedProj.GetComponent<Rigidbody2D>();
            launchedProj.GetComponent<BasicProjectile>().damage = damage;
            if(!launchedProj.GetComponent<BasicProjectile>().damageTypes.Contains(damageType.ToString())){
                launchedProj.GetComponent<BasicProjectile>().damageTypes.Add(damageType.ToString());
            }
            if(projRB != null){
                projRB.AddForce(this.transform.up * projectileSpeed, ForceMode2D.Impulse);
            }
            if(launchedProj.GetComponent<BasicProjectile>() != null){
                launchedProj.GetComponent<BasicProjectile>().cam = cam;
                launchedProj.GetComponent<BasicProjectile>().hitShake = hitShake;
                launchedProj.GetComponent<BasicProjectile>().shakeAmount = shakeAmount;
                launchedProj.GetComponent<BasicProjectile>().shakeDuration = shakeDuration;
            }
        }
    }
    private void Update(){
        if(cam == null){
            cam = FindObjectOfType<CameraShake>();
        }
    }
}
