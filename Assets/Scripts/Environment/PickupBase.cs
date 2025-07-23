using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupBase : MonoBehaviour
{
    [SerializeField][Range(-5, 5f)] float minForce;
    [SerializeField][Range(-5, 5f)] float maxForce;
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Generate a random direction
        Vector3 randomDirection = UnityEngine.Random.onUnitSphere;

        // Choose a random force magnitude
        float randomForce = UnityEngine.Random.Range(minForce, maxForce);

        // Apply the impulse
        rb.AddForce(randomDirection * randomForce, ForceMode2D.Impulse);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            if (other.gameObject.GetComponent<ObjectTags>().tags.ToString().Contains("Player"))
            {
                PickupBehavior();
            }
        }
        catch (Exception e)
        {
        }
    }
    public virtual void PickupBehavior()
    {
        Destroy(this.gameObject);
    }
}
