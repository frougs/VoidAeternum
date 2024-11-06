using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageTypes : MonoBehaviour
{
    [Flags]
    public enum DamageType
    {
        None = 0,
        Physical = 1 << 0,  // 1
        Electric = 1 << 1,  // 2
        Explosive = 1 << 2, // 4
        Piercing = 1 << 3,  // 8
        Magic = 1 << 4      // 16
    }
}
