using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Asteroid : DestructableObject
{
    [HideInInspector] public AsteroidSpawner spawner;
    [SerializeField] private float baseCollisionDamage;
    private GameObject player;
    public override void OnDestruction()
    {
        base.OnDestruction();
        //spawner.totalCurrentlySpawned -= 1;
        if (destroyParticles != null)
        {
            Instantiate(destroyParticles, this.transform.position, Quaternion.identity);
        }
        try
        {
            cShaker.Shake(shakeIntensity, shakeDuration);
            spawner.ResetAsteroid(this.gameObject);
        }
        catch (Exception e)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.GetComponent<ObjectTags>() != null)
        {
            if (collision.transform.gameObject.GetComponent<ObjectTags>().tags.ToString().Contains("Player"))
            {
                player = collision.transform.gameObject;
                OnDestruction();
                player.GetComponent<PlayerStats>().TakeDamage(baseCollisionDamage * sizeAndHPMultiplier);
                //Add player damage here l8r based on asteroid size multiplier :}
            }
        }
    }
}
