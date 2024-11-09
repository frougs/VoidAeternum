using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : BaseWeapon, IReleasable
{
    [Header("Mining Laser Info")]
    [SerializeField] GameObject laser;
    [SerializeField] GameObject laserOrigin;
    [SerializeField] GameObject hitParticles;
    [SerializeField] float range;
    [SerializeField] LayerMask IgnoreLayer;
    private LaserCollision obj;
    private LineRenderer line;

    private void Update(){
        if(obj == null){
            obj = laser.GetComponent<LaserCollision>();
            obj.damage = damage;
            obj.firerate = firerate;
            obj.damageTypes.Add(damageType.ToString());
            obj.hitParticles = hitParticles;
            obj.range = range;
            obj.IgnoreLayer = IgnoreLayer;
            obj.hitShake = hitShake;
            obj.shakeAmount = shakeAmount;
            obj.shakeDuration = shakeDuration;
            line = obj.GetComponent<LineRenderer>();
        }
        if(cam == null){
            cam = FindObjectOfType<CameraShake>();
            obj.cam = cam;
        }
    }
    public override void Shot(){
        if(canShoot){
            obj.ShootLaser(laserOrigin);
            StartCoroutine(ShotDelay());
        }
    }
    public void ShotReleased(){
        line.positionCount = 0;
    }
}
