using UnityEngine;
using System.Collections.Generic;

public class WeaponSlotManager : MonoBehaviour
{
    [SerializeField] GameObject emptyWeaponHolder;
    [SerializeField] GameObject leftSide;
    [SerializeField] GameObject rightSide;

    private Dictionary<GameObject, WeaponUISlot> leftHolders = new Dictionary<GameObject, WeaponUISlot>();
    private Dictionary<GameObject, WeaponUISlot> rightHolders = new Dictionary<GameObject, WeaponUISlot>();

    public void UpdateWeaponUI(int slots, Dictionary<GameObject, GameObject> weapons, string side)
    {
        if (side == "Left")
        {
            CheckContainers(leftSide, leftHolders, weapons);
        }
        if (side == "Right")
        {
            CheckContainers(rightSide, rightHolders, weapons);
        }
    }

    private void CheckContainers(GameObject side, Dictionary<GameObject, WeaponUISlot> holders, Dictionary<GameObject, GameObject> weapons)
    {
        foreach (var kvp in weapons)
        {
            GameObject slotKey = kvp.Key;
            GameObject weaponValue = kvp.Value;

            holders.TryGetValue(slotKey, out WeaponUISlot holder);

            if (weaponValue == null)
            {
                // Slot is empty - if the old holder still shows a weapon, replace it with a fresh one
                if (holder != null && holder.hasWeapon)
                {
                    Destroy(holder.gameObject);
                    GameObject freshHolder = Instantiate(emptyWeaponHolder, side.transform);
                    holders[slotKey] = freshHolder.GetComponent<WeaponUISlot>();
                }
                else if (holder == null)
                {
                    GameObject newHolder = Instantiate(emptyWeaponHolder, side.transform);
                    holders[slotKey] = newHolder.GetComponent<WeaponUISlot>();
                }
                // else: holder already exists and is already empty, leave it alone
            }
            else
            {
                if (holder == null)
                {
                    GameObject newHolder = Instantiate(emptyWeaponHolder, side.transform);
                    holder = newHolder.GetComponent<WeaponUISlot>();
                    holders[slotKey] = holder;
                }

                // Don't overwrite a weapon that's already assigned to this holder
                if (!holder.hasWeapon)
                {
                    holder.weapon = weaponValue;
                    holder.UpdateDisplay();
                }
            }
        }

        // Remove holders whose slot no longer exists in the incoming dictionary at all
        List<GameObject> keysToRemove = new List<GameObject>();
        foreach (var slotKey in holders.Keys)
        {
            if (!weapons.ContainsKey(slotKey))
            {
                keysToRemove.Add(slotKey);
            }
        }

        foreach (var slotKey in keysToRemove)
        {
            Destroy(holders[slotKey].gameObject);
            holders.Remove(slotKey);
        }
    }
}