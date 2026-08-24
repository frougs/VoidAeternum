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
    public WingSide Side => side;

    public int weaponContainers;

    private Dictionary<GameObject, GameObject> containers = new Dictionary<GameObject, GameObject>();

    private void OnEnable()
    {
        Refresh();
    }

    public void UpdateWeaponSlots()
    {
        WeaponUISingleton.instance.GetComponent<WeaponSlotManager>().UpdateWeaponUI(weaponContainers, containers, side.ToString());
    }

    public void Refresh()
    {
        containers.Clear();
        var slots = this.GetComponentsInChildren<WeaponSlot>();

        foreach (WeaponSlot slot in slots)
        {
            GameObject weapon = slot.gameObject.transform.childCount != 0
                ? slot.gameObject.transform.GetChild(0).gameObject
                : null;

            containers[slot.gameObject] = weapon;
        }

        weaponContainers = containers.Count;
        UpdateWeaponSlots();
    }
}