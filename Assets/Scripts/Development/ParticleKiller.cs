using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour
{
    private ParticleSystem ps;
    private void Start(){
        ps = this.GetComponent<ParticleSystem>();
        Destroy(this.gameObject, 0.8f);
    }
    private void Update(){
        if(ps != null){
            if(!ps.IsAlive()){
                Destroy(this.gameObject);
            }
        }
    }
}
