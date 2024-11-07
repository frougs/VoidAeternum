using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float damage;
    public List<string> damageTypes = new List<string>();
    [SerializeField] private bool reportHitData;
    [SerializeField] GameObject particleHit;
    //[SerializeField] string[] damagetype = new string[];

    private void OnTriggerEnter2D(Collider2D other){
        if(reportHitData){
            Debug.Log("Hit: " +other.gameObject.name);
        }
        var objTags = other.gameObject.GetComponent<ObjectTags>();
        if(objTags != null){
            if(objTags.tags.ToString().Contains("Player") || objTags.tags.ToString().Contains("System")){
                return;
            }
        }
        var damagable = other.gameObject.GetComponent<IDamagable>();
        if(damagable != null){
            damagable.Damaged(damage, damageTypes);
            HitSomething();
        }
        else{
            HitSomething();
        }
    }
    private void HitSomething(){
        Instantiate(particleHit, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
