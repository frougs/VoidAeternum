using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupBase : ObjectID
{
    [SerializeField][Range(-5, 5f)] float minForce;
    [SerializeField][Range(-5, 5f)] float maxForce;
    [SerializeField] GameObject pickupParticles;
    [SerializeField] bool canDespawn = true;
    [SerializeField] float despawnTime = 3f;
    [SerializeField] float delayBeforeDisappear = 2f;
    private Vector3 initialScale;
    //[HideInInspector] public GameObject player;
    public virtual void Start()
    {
        initialScale = transform.localScale;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Generate a random direction
        Vector3 randomDirection = UnityEngine.Random.onUnitSphere;

        // Choose a random force magnitude
        float randomForce = UnityEngine.Random.Range(minForce, maxForce);

        // Apply the impulse
        rb.AddForce(randomDirection * randomForce, ForceMode2D.Impulse);
        if (canDespawn)
        {
            StartCoroutine(Disappear());
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            if (other.gameObject.GetComponent<ObjectTags>().tags.ToString().Contains("Player"))
            {
                //player = other.gameObject;
                PickupBehavior(other.gameObject);
            }
        }
        catch (Exception e)
        {
        }
    }
    public virtual void PickupBehavior(GameObject obj)
    {
        if(pickupParticles != null){
            Instantiate(pickupParticles, this.transform.position, Quaternion.identity);
        }
        ObjectPoolerSingleton.instance.GetComponent<ObjectPooler>().ReturnObjectToPool(this.gameObject);
    }

   private IEnumerator Disappear()
    {
        if (delayBeforeDisappear > 0f)
            yield return new WaitForSeconds(delayBeforeDisappear);

        float shrinkDuration = despawnTime - delayBeforeDisappear;
        shrinkDuration = Mathf.Max(shrinkDuration, 0f);

        float elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / shrinkDuration);
            float scaleFactor = Mathf.Lerp(1f, 0f, t);
            transform.localScale = initialScale * scaleFactor;
            yield return null;
        }

        transform.localScale = Vector3.zero;


        transform.localScale = initialScale;

        ObjectPoolerSingleton.instance.GetComponent<ObjectPooler>().ReturnObjectToPool(this.gameObject);
    }
}
