using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : DestructableObject
{
    public override void OnDestruction(){
        
        Destroy(this.gameObject);
    }
}
