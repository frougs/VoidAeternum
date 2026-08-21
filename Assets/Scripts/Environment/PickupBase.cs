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
    [SerializeField] float delayBeforeDisappear = 2f;
    [SerializeField] float shrinkDuration = 1f;
    [SerializeField] GameObject poofParticles;
    private Vector3 initialScale;
    private Rigidbody2D rb;
    private Coroutine disappearRoutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = initialScale;

        Vector3 randomDirection = UnityEngine.Random.onUnitSphere;
        float randomForce = UnityEngine.Random.Range(minForce, maxForce);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(randomDirection * randomForce, ForceMode2D.Impulse);

        if (canDespawn && !isPooled)
        {
            disappearRoutine = StartCoroutine(Disappear());
        }
    }

    private void OnDisable()
    {
        if (disappearRoutine != null)
        {
            StopCoroutine(disappearRoutine);
            disappearRoutine = null;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            if (other.gameObject.GetComponent<ObjectTags>().tags.ToString().Contains("Player"))
            {
                PickupBehavior(other.gameObject);
            }
        }
        catch (Exception e)
        {
        }
    }

    public virtual void PickupBehavior(GameObject obj)
    {
        if (pickupParticles != null)
        {
            Instantiate(pickupParticles, this.transform.position, Quaternion.identity);
        }
        ObjectPoolerSingleton.instance.GetComponent<ObjectPooler>().ReturnObjectToPool(this.gameObject);
    }

    private IEnumerator Disappear()
    {
        if (delayBeforeDisappear > 0f)
            yield return new WaitForSeconds(delayBeforeDisappear);

        float elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / shrinkDuration);
            transform.localScale = initialScale * Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        transform.localScale = Vector3.zero;
        transform.localScale = initialScale;
        Instantiate(poofParticles, this.transform.position, Quaternion.identity);
        ObjectPoolerSingleton.instance.GetComponent<ObjectPooler>().ReturnObjectToPool(this.gameObject);
    }
}