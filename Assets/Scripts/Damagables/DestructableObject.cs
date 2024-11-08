using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[RequireComponent(typeof(DamageTypes))]
public class DestructableObject : MonoBehaviour, IDamagable
{
    public DamageTypes.DamageType vulnerabilities;
    public DamageTypes.DamageType immunities;
    public float currentHealth;
    public float maxHealth;
    [SerializeField] [Range(-1, -0.1f)] float minFallSpeed;
    [SerializeField] [Range(-1,-0.1f)] float maxFallSpeed;
    private float fallSpeed;
    public GameObject destroyParticles;
    public CameraShake cShaker;
    public float shakeIntensity;
    public float shakeDuration;

    private void Start(){
        currentHealth = maxHealth;
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        this.GetComponent<ConstantForce2D>().force = new Vector2(0, fallSpeed);
        cShaker = FindObjectOfType<CameraShake>();
    }

    public void Damaged(float dmg, List<string> damageTypes){
        bool damagedOnce = false;
        foreach(var type in damageTypes){
            if(!immunities.ToString().Contains(type.ToString()) && !damagedOnce){
                //Debug.Log("This will be damaged for: " +dmg.ToString());
                damagedOnce = true;
                currentHealth -= dmg;
            }
            else{
                //Debug.Log("This is immune to this damage type: " +type.ToString());
            }
        }
    }
    private void Update(){
        if(currentHealth <= 0){
            OnDestruction();
        }
    }
    public virtual void OnDestruction(){
    }
    private void FixedUpdate(){
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fallSpeed);
    }

}
