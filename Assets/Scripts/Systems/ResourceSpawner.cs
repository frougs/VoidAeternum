using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class SpawnableItems
{
    public GameObject item;
    public float spawnChance;
    public int minSpawnAmount;
    public int maxSpawnAmount;
}
public class ResourceSpawner : MonoBehaviour
{
    public SpawnableItems[] spawnableItems;

    public GameObject exp;
    public int minExp;
    public int maxExp;

    public void SpawnItems()
    {
        //Debug.Log("Spawning resources...");


        float totalChance = 0f;
        foreach (var item in spawnableItems)
        {
            totalChance += item.spawnChance;
        }

        if (totalChance <= 0f)
        {
            Debug.LogWarning("Total spawn chance is 0. Nothing will be spawned.");
            return;
        }


        float randomValue = Random.Range(0f, totalChance);
        float cumulative = 0f;


        foreach (var item in spawnableItems)
        {
            cumulative += item.spawnChance;
            if (randomValue <= cumulative)
            {

                int amountToSpawn = Random.Range(item.minSpawnAmount, item.maxSpawnAmount + 1);

                for (int i = 0; i < amountToSpawn; i++)
                {
                    //Instantiate(item.item, this.transform.position, Quaternion.identity);
                    GameObject spawnedResource = ObjectPoolerSingleton.instance.GetComponent<ObjectPooler>().SpawnPooledObject(item.item);
                    spawnedResource.transform.position = this.transform.position;
                }
                return; 
            }
        }

        // This should never be reached
        Debug.LogWarning("No item was selected even though total chance > 0. Check spawn chances.");
    }

}
