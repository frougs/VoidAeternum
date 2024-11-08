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
            line = obj.GetComponent<LineRenderer>();
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
