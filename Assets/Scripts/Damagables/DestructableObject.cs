using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[RequireComponent(typeof(DamageTypes))]
public class DestructableObject : MonoBehaviour, IDamagable
{
    public DamageTypes.DamageType vulnerabilities;
    public DamageTypes.DamageType immunities;
    [SerializeField] float health;

    public void Damaged(float dmg, List<string> damageTypes){
        bool damagedOnce = false;
        foreach(var type in damageTypes){
            if(!immunities.ToString().Contains(type.ToString()) && !damagedOnce){
                //Debug.Log("This will be damaged for: " +dmg.ToString());
                damagedOnce = true;
                health -= dmg;
            }
            else{
                Debug.Log("This is immune to this damage type: " +type.ToString());
            }
        }
    }
    private void Update(){
        if(health <= 0){
            OnDestruction();
        }
    }
    public virtual void OnDestruction(){
    }

}
