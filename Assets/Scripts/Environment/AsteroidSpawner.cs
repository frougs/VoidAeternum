using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval;
    public bool canSpawn = true;
    [SerializeField] GameObject asteroidOBJ;
    [HideInInspector] public int totalCurrentlySpawned = 0;
    [SerializeField] int maxAsteroidCount;
    [SerializeField] GameObject asteroidParent;
    private void Start(){
        StartCoroutine(SpawnAsteroid());
    }
    private IEnumerator SpawnAsteroid(){
        //Debug.Log("Coroutine Starting");
        yield return new WaitForSeconds(spawnInterval);
        Spawn(this.gameObject.GetComponent<BoxCollider2D>().bounds);
    }
    private void Spawn(Bounds bounds){
        if(canSpawn && totalCurrentlySpawned < maxAsteroidCount){
            //Debug.Log("Attempting spawn");
            Vector2 spawnArea = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
            //Debug.Log("Spawn POS: " +spawnArea.ToString());
            var spawned = Instantiate(asteroidOBJ, spawnArea, Quaternion.identity);
            spawned.transform.SetParent(asteroidParent.transform);
            spawned.GetComponent<Asteroid>().spawner = this;
            totalCurrentlySpawned += 1;
        }
        StartCoroutine(SpawnAsteroid());
    }
    public void ResetAsteroid(GameObject asteroid){
        asteroidOBJ.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        var asteroidScript = asteroid.GetComponent<Asteroid>();
        asteroidScript.currentHealth = asteroidScript.maxHealth;
        var thisBounds = this.gameObject.GetComponent<BoxCollider2D>().bounds;
        asteroid.transform.position = new Vector2(Random.Range(thisBounds.min.x, thisBounds.max.x), Random.Range(thisBounds.min.y, thisBounds.max.y));
    }
}
