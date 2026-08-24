using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class WeaponUISlot : MonoBehaviour, IDropHandler
{
    public GameObject weapon;
    public bool hasWeapon = false;
    [SerializeField] Image sprite;
    [SerializeField] GameObject plusSign;
    [SerializeField] string weaponName;

    [HideInInspector] public GameObject slotKey;
    [HideInInspector] public string side;

    private Action<WeaponUISlot, UpgradeHolder> onItemDropped;

    public void Initialize(GameObject slotKey, string side, Action<WeaponUISlot, UpgradeHolder> onItemDropped)
    {
        this.slotKey = slotKey;
        this.side = side;
        this.onItemDropped = onItemDropped;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (hasWeapon) return;
        if (eventData.pointerDrag == null) return;

        UpgradeHolder droppedHolder = eventData.pointerDrag.GetComponent<UpgradeHolder>();
        if (droppedHolder == null || droppedHolder.UpgradeItem == null) return;

        ObjectID objectID = droppedHolder.UpgradeItem.GetComponent<ObjectID>();
        if (objectID == null || objectID.equipable != "wing") return;

        onItemDropped?.Invoke(this, droppedHolder);
    }

    public void UpdateDisplay()
    {
        sprite.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        plusSign.SetActive(false);
        hasWeapon = true;
    }

    public void ClearDisplay()
    {
        weapon = null;
        sprite.sprite = null;
        plusSign.SetActive(true);
        hasWeapon = false;
    }
}