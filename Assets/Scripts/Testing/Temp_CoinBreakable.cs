using UnityEngine;

public class Temp_CoinBreakable : DestructableObject
{
    public override void OnDestruction()
    {
        base.OnDestruction();
        Destroy(this.gameObject);
    }
}
