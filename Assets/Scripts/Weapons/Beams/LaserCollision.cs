using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public float firerate;
    [HideInInspector] public float range;
    public LayerMask IgnoreLayer;
    public List<string> damageTypes = new List<string>();
    public GameObject hitParticles;
    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public void ShootLaser(GameObject laserOrigin)
    {
        line.positionCount = 0;
        line.positionCount++;
        line.SetPosition(0, Vector2.zero);
        line.positionCount++;
        RaycastHit2D hitData = Physics2D.Raycast(laserOrigin.transform.position, transform.up, range, ~IgnoreLayer);
        //Debug.DrawRay(laserOrigin.transform.position, transform.up * range, Color.green);
        if (hitData.collider != null)
        {
            var hitPoint = new Vector2(0f, (hitData.point.y - laserOrigin.transform.position.y) * 2f);
            line.SetPosition(1, hitPoint);
            IDamagable damagable = hitData.transform.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                FindObjectOfType<CameraShake>().Shake(0.35f, 0.08f);
                damagable.Damaged(damage, damageTypes);
            }

            if (hitData.transform.gameObject != null && hitParticles != null)
            {
                Instantiate(hitParticles, hitData.point, Quaternion.identity);
            }
        }
        else{
            //Debug.Log("Adding point to range");
            Vector2 lineEnd = new Vector2(0, range * 2f);
            line.SetPosition(1, lineEnd);
        }
    }


}
