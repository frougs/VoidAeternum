using UnityEngine;

public class EXPPickup : AttractionPickup
{
   
    public float expValue;

    public override void PickupBehavior(GameObject obj)
    {
        base.PickupBehavior(obj);
        player.GetComponent<ExpManager>().AddExp(expValue);
    }
}
