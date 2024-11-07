using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : DestructableObject
{
    [HideInInspector] public AsteroidSpawner spawner;
    public override void OnDestruction(){
        //spawner.totalCurrentlySpawned -= 1;
        if(destroyParticles != null){
            Instantiate(destroyParticles, this.transform.position, Quaternion.identity);
        }
        cShaker.Shake(shakeIntensity, shakeDuration);
        spawner.ResetAsteroid(this.gameObject);
        //Destroy(this.gameObject);
    }
}
