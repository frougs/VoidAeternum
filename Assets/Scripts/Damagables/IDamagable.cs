using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable{
    void Damaged(float dmg, List<string> damageTypes);
}

