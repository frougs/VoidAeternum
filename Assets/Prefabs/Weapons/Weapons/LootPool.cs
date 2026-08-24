using UnityEngine;

[CreateAssetMenu(fileName = "LootPool", menuName = "Scriptable Objects/LootPool")]
public class LootPool : ScriptableObject
{
    public string lootPoolName;

    [System.Serializable]
    public class Loot
    {
        public GameObject objPrefab;
        public float rarity;
    }

    public Loot[] lootable;
}