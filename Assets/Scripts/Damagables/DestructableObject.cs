using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

//[RequireComponent(typeof(DamageTypes))]
public class DestructableObject : MonoBehaviour, IDamagable
{
    public DamageTypes.DamageType vulnerabilities;
    public float currentHealth;
    public float maxHealth;
    [SerializeField][Range(-1, -0.1f)] float minFallSpeed;
    [SerializeField][Range(-1, -0.1f)] float maxFallSpeed;
    private float fallSpeed;
    public GameObject destroyParticles;
    public CameraShake cShaker;
    public float shakeIntensity;
    public float shakeDuration;
    public UnityEvent objectDestroyed;
    [SerializeField] private bool randomizeSizeAndHP;
    [SerializeField] private float minSizeMultiplier;
    [SerializeField] private float maxSizeMultiplier;
    [HideInInspector] public float sizeAndHPMultiplier;
    [SerializeField] bool dropExp;
    [SerializeField] EXPSpawner xpSpawner;
    [SerializeField] int minExp;
    [SerializeField] int maxExp;

    private void Start()
    {
        ObjectSetup();
        cShaker = FindObjectOfType<CameraShake>();
    }

    public void Damaged(float dmg, List<string> damageTypes)
    {
        bool damagedOnce = false;
        if (!damagedOnce)
        {
            foreach (var type in damageTypes)
            {
                if (vulnerabilities.ToString().Contains(type))
                {
                    currentHealth -= dmg * 1.25f;
                    return;
                }
                else
                {
                    currentHealth -= dmg;
                    return;
                }
            }
        }
        damagedOnce = true;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            OnDestruction();
        }
    }
    public virtual void OnDestruction()
    {
        if (dropExp && xpSpawner != null)
        {
            xpSpawner.SpawnEXP(Mathf.CeilToInt(minExp * sizeAndHPMultiplier), Mathf.CeilToInt(maxExp * sizeAndHPMultiplier));
        }
        objectDestroyed?.Invoke();
    }
    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, fallSpeed);
    }
    public virtual void ObjectSetup()
    {
        this.gameObject.transform.localScale = new Vector3(2.29f, 2.29f, 2.29f);
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);

        if (randomizeSizeAndHP)
        {
            sizeAndHPMultiplier = Random.Range(minSizeMultiplier, maxSizeMultiplier);
            currentHealth = maxHealth * sizeAndHPMultiplier;
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * sizeAndHPMultiplier, this.gameObject.transform.localScale.y * sizeAndHPMultiplier, this.gameObject.transform.localScale.z * sizeAndHPMultiplier);
            float modifiedFallSpeed = fallSpeed / sizeAndHPMultiplier;
            this.GetComponent<ConstantForce2D>().force = new Vector2(0, modifiedFallSpeed);
        }
        else
        {
            currentHealth = maxHealth;
            this.GetComponent<ConstantForce2D>().force = new Vector2(0, fallSpeed);
        }
    }

}
