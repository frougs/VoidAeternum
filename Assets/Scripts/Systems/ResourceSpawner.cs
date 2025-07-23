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

    public void SpawnItems()
    {
        Debug.Log("Spawning resources...");

        // Calculate total weight
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

        // Roll a random number within totalChance range
        float randomValue = Random.Range(0f, totalChance);
        float cumulative = 0f;

        // Select an item based on weighted random selection
        foreach (var item in spawnableItems)
        {
            cumulative += item.spawnChance;
            if (randomValue <= cumulative)
            {
                // Determine how much to spawn
                int amountToSpawn = Random.Range(item.minSpawnAmount, item.maxSpawnAmount + 1);

                for (int i = 0; i < amountToSpawn; i++)
                {
                    Instantiate(item.item, this.transform.position, Quaternion.identity);
                }

                return; // Exit after spawning one item
            }
        }

        // This should never be reached
        Debug.LogWarning("No item was selected even though total chance > 0. Check spawn chances.");
    }

}
