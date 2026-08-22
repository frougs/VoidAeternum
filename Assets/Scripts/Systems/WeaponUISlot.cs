using UnityEngine;
using UnityEngine.UI;

public class WeaponUISlot : MonoBehaviour
{
    public GameObject weapon;
    public bool hasWeapon = false;
    [SerializeField] Image sprite;
    [SerializeField] GameObject plusSign;
    [SerializeField] string weaponName;

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