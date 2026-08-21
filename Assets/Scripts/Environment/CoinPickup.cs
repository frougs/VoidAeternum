using UnityEngine;

public class CoinPickup : AttractionPickup
{
    public int moneyValue;

    public override void PickupBehavior(GameObject obj)
    {
        base.PickupBehavior(obj);
        player.GetComponent<MoneyManager>().CollectMoney(moneyValue);
    }
}
