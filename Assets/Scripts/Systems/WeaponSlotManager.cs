using UnityEngine;
using System.Collections.Generic;
using System;

public class WeaponSlotManager : MonoBehaviour
{
    [SerializeField] GameObject emptyWeaponHolder;
    [SerializeField] GameObject leftSide;
    [SerializeField] GameObject rightSide;

    private Dictionary<GameObject, WeaponUISlot> leftHolders = new Dictionary<GameObject, WeaponUISlot>();
    private Dictionary<GameObject, WeaponUISlot> rightHolders = new Dictionary<GameObject, WeaponUISlot>();

    private Action<GameObject, string, UpgradeHolder> onItemDropped;

    public void RegisterDropCallback(Action<GameObject, string, UpgradeHolder> callback)
    {
        onItemDropped = callback;
    }

    public void UpdateWeaponUI(int slots, Dictionary<GameObject, GameObject> weapons, string side)
    {
        if (side == "Left")
        {
            CheckContainers(leftSide, leftHolders, weapons, side);
        }
        if (side == "Right")
        {
            CheckContainers(rightSide, rightHolders, weapons, side);
        }
    }

    private void CheckContainers(GameObject sideContainer, Dictionary<GameObject, WeaponUISlot> holders, Dictionary<GameObject, GameObject> weapons, string side)
    {
        foreach (var kvp in weapons)
        {
            GameObject slotKey = kvp.Key;
            GameObject weaponValue = kvp.Value;

            holders.TryGetValue(slotKey, out WeaponUISlot holder);

            if (weaponValue == null)
            {
                if (holder != null && holder.hasWeapon)
                {
                    Destroy(holder.gameObject);
                    holder = CreateHolder(sideContainer, slotKey, side, holders);
                }
                else if (holder == null)
                {
                    holder = CreateHolder(sideContainer, slotKey, side, holders);
                }
            }
            else
            {
                if (holder == null)
                {
                    holder = CreateHolder(sideContainer, slotKey, side, holders);
                }

                if (!holder.hasWeapon)
                {
                    holder.weapon = weaponValue;
                    holder.UpdateDisplay();
                }
            }
        }

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

    private WeaponUISlot CreateHolder(GameObject sideContainer, GameObject slotKey, string side, Dictionary<GameObject, WeaponUISlot> holders)
    {
        GameObject newHolder = Instantiate(emptyWeaponHolder, sideContainer.transform);
        WeaponUISlot slot = newHolder.GetComponent<WeaponUISlot>();
        slot.Initialize(slotKey, side, HandleItemDropped);
        holders[slotKey] = slot;
        return slot;
    }

    private void HandleItemDropped(WeaponUISlot slot, UpgradeHolder droppedHolder)
    {
        onItemDropped?.Invoke(slot.slotKey, slot.side, droppedHolder);
    }
}