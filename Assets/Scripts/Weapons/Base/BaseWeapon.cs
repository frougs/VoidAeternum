using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(DamageTypes))]
public class BaseWeapon : MonoBehaviour, IShootable
{
    [HideInInspector] public bool canShoot = true;
    [SerializeField] public float firerate;
    [SerializeField] public float damage;
    public bool hitShake;
    public float shakeAmount;
    public float shakeDuration;
    public DamageTypes.DamageType damageType;
    [HideInInspector] public CameraShake cam;
    public virtual void Shot(){
        if(canShoot){
            StartCoroutine(ShotDelay());
        }
    }
    public IEnumerator ShotDelay(){
        canShoot = false;
        yield return new WaitForSeconds(firerate);
        canShoot = true;
    }
}
