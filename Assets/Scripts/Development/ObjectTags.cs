using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectTags : MonoBehaviour
{
    [Flags]
    public enum Tags
    {
        None = 0,
        Player = 1 << 0,  // 1
        System = 1 << 1,
        Resource = 1 << 2,
        Exp = 1 << 3
        // Electric = 1 << 1,  // 2
        // Explosive = 1 << 2, // 4
        // Piercing = 1 << 3,  // 8
        // Magic = 1 << 4      // 16
    }
    public Tags tags;
}
