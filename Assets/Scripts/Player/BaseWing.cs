using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class BaseWing : MonoBehaviour
{
    public enum WingSide
    {
        Left,
        Right
    }
    [SerializeField] private WingSide side;
    public int weaponContainers;
    //public HashSet<GameObject> weaponSlots = new HashSet<GameObject>();

    //public UnityEvent<int, GameObject[], string> updateWeaponSlots;

    private Dictionary<GameObject, GameObject> containers = new Dictionary<GameObject, GameObject>();

    private void OnEnable()
    {
        Refresh();
    }

    public void UpdateWeaponSlots()
    {
        //updateWeaponSlots.Invoke(weaponContainers, weaponSlots, side.ToString());
        WeaponUISingleton.instance.GetComponent<WeaponSlotManager>().UpdateWeaponUI(weaponContainers, containers, side.ToString());
    }

    public void Refresh()
    {
        containers.Clear();
        var slots = this.GetComponentsInChildren<WeaponSlot>();
        //Debug.Log("Checking detected slots on " + side.ToString() + " side: " + slots.Length);

        foreach (WeaponSlot slot in slots)
        {
            GameObject weapon = slot.gameObject.transform.childCount != 0
                ? slot.gameObject.transform.GetChild(0).gameObject
                : null;

            if (!containers.ContainsKey(slot.gameObject))
            {
                //Debug.LogWarning("Key doesnt exist, adding...");
            }
            else
            {
                //Debug.LogWarning("Key already exists, updating weapon...");
            }

            // Indexer handles both insert and update, so this is always safe
            containers[slot.gameObject] = weapon;
        }

        weaponContainers = containers.Count;
        UpdateWeaponSlots();
    }
}