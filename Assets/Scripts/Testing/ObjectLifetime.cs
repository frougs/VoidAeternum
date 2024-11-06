using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifetime : MonoBehaviour
{
    [SerializeField] float lifeTime;
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

}
