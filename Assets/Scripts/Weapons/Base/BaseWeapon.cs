using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(DamageTypes))]
public class BaseWeapon : MonoBehaviour, IShootable
{
    [HideInInspector] public bool canShoot = true;
    [SerializeField] public float firerate;
    [SerializeField] public float damage;
    public DamageTypes.DamageType damageType;
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
