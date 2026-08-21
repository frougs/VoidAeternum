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
    public GameObject[] weaponSlots;

    public UnityEvent<int, GameObject[], string> updateWeaponSlots;

    private Dictionary<GameObject, int> containers = new Dictionary<GameObject, int>();

    private void OnEnable()
    {
        weaponContainers = weaponSlots.Length;
        updateWeaponSlots.Invoke(weaponContainers, weaponSlots, side.ToString());
    }

}
