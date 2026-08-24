using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public List<LootPool> lootPools = new List<LootPool>();
    public bool tempTriggerUpgrade = false;
    [SerializeField] GameObject upgradeHolderPrefab;
    [SerializeField] GameObject upgradeParent;
    [SerializeField] GameObject upgradeMenu;
    [SerializeField] WeaponSlotManager weaponSlotManager;

    private List<GameObject> spawnedHolders = new List<GameObject>();

    private void Start()
    {
        if (weaponSlotManager != null)
        {
            weaponSlotManager.RegisterDropCallback(OnItemDropped);
        }
    }

    public void TriggerUpgrade(int ignore, int numOfUpgrades)
    {
        if (lootPools == null || lootPools.Count == 0)
        {
            Debug.LogWarning("No loot pools assigned to UpgradeManager.");
            return;
        }

        upgradeMenu.SetActive(true);

        List<LootPool.Loot> combinedLoot = BuildCombinedLootList();

        if (combinedLoot.Count == 0)
        {
            Debug.LogWarning("Combined loot list is empty.");
            return;
        }

        for (int i = 0; i < numOfUpgrades; i++)
        {
            GameObject selectedItem = GetRandomItem(combinedLoot);
            if (selectedItem == null) continue;

            GameObject holder = Instantiate(upgradeHolderPrefab, upgradeParent.transform);
            spawnedHolders.Add(holder);

            UpgradeHolder upgradeHolder = holder.GetComponent<UpgradeHolder>();
            if (upgradeHolder != null)
            {
                upgradeHolder.LoadUpgrade(selectedItem);
            }
            else
            {
                Debug.LogWarning("upgradeHolderPrefab is missing an UpgradeHolder component.");
            }
        }
    }

    private void OnItemDropped(GameObject slotKey, string side, UpgradeHolder droppedHolder)
    {
        GameObject upgradeItem = droppedHolder.UpgradeItem;
        if (upgradeItem == null || slotKey == null) return;

        GameObject weaponInstance = Instantiate(upgradeItem, slotKey.transform);
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localRotation = Quaternion.identity;

        BaseWing wing = GetWingForSide(side);
        if (wing != null)
        {
            wing.Refresh();
        }
        else
        {
            Debug.LogWarning($"Could not find a BaseWing for side '{side}'.");
        }

        droppedHolder.MarkConsumed();
        ClearUpgradeHolders();
    }

    private BaseWing GetWingForSide(string side)
    {
        if (PlayerSingleton.instance == null)
        {
            Debug.LogWarning("PlayerSingleton instance not found.");
            return null;
        }

        ShipVisualUpdater visualUpdater = PlayerSingleton.instance.GetComponent<ShipVisualUpdater>();
        if (visualUpdater == null)
        {
            Debug.LogWarning("Player is missing a ShipVisualUpdater component.");
            return null;
        }

        if (!System.Enum.TryParse(side, out BaseWing.WingSide wingSide))
        {
            Debug.LogWarning($"'{side}' is not a valid WingSide.");
            return null;
        }

        return visualUpdater.GetWing(wingSide);
    }

    private void ClearUpgradeHolders()
    {
        foreach (var holder in spawnedHolders)
        {
            if (holder != null)
            {
                Destroy(holder);
            }
        }
        spawnedHolders.Clear();
        upgradeMenu.SetActive(false);
    }

    public void AddLootPool(LootPool pool)
    {
        if (pool == null) return;
        if (!lootPools.Contains(pool))
        {
            lootPools.Add(pool);
        }
    }

    public void RemoveLootPool(LootPool pool)
    {
        if (pool == null) return;
        lootPools.Remove(pool);
    }

    private List<LootPool.Loot> BuildCombinedLootList()
    {
        List<LootPool.Loot> combined = new List<LootPool.Loot>();
        foreach (LootPool pool in lootPools)
        {
            if (pool == null || pool.lootable == null) continue;
            combined.AddRange(pool.lootable);
        }
        return combined;
    }

    private GameObject GetRandomItem(List<LootPool.Loot> lootList)
    {
        float totalWeight = 0f;
        foreach (LootPool.Loot loot in lootList)
        {
            totalWeight += Mathf.Max(0f, loot.rarity);
        }

        if (totalWeight <= 0f)
        {
            Debug.LogWarning("Total weight of loot list is 0.");
            return null;
        }

        float roll = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (LootPool.Loot loot in lootList)
        {
            cumulative += Mathf.Max(0f, loot.rarity);
            if (roll <= cumulative)
            {
                return loot.objPrefab;
            }
        }

        return lootList[lootList.Count - 1].objPrefab;
    }

    public void Update()
    {
        if (tempTriggerUpgrade)
        {
            tempTriggerUpgrade = false;
            TriggerUpgrade(0, 1);
        }
    }
}