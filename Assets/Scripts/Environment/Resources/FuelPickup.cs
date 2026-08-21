using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickup : PickupBase
{
    [SerializeField] float fuelAmount;
    public override void PickupBehavior(GameObject obj)
    {
        base.PickupBehavior(obj);
        obj.GetComponentInChildren<EngineScript>().AddFuel(fuelAmount);
    }
}
