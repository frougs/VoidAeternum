using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<Asteroid>() != null){
            FindObjectOfType<AsteroidSpawner>().ResetAsteroid(other.gameObject);
        }
        else{
            Destroy(other.gameObject);
        }
    }
}
