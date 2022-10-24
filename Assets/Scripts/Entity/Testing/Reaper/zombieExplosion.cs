using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieExplosion : MonoBehaviour
{
    public float lifespan;
    public float lifespanCounter;
    public float explosionDamage;
    public float bossExplosionDamage;
    public float explosionRadius;
    
    void Start()
    {
        DamageFallOff(transform.position, explosionRadius, explosionDamage);
    }

    void Update()
    {
        if(lifespanCounter >= lifespan)
        {
            Destroy(gameObject);
        }
        lifespanCounter += (Time.deltaTime);
    }

    private void DamageFallOff(Vector3 location,float radius,float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            Debug.Log("zombie explosion hit: " + col);
            if (col.CompareTag("hordeEnemy")) 
            {
                col.GetComponent<hordePathing>().takeDamage(damage);
                continue;
            }
            if (col.CompareTag("enemyHitbox"))
            {
                //float[] effectPasser = { damage * (((location - col.transform.position).magnitude / radius) - 1) };
                float[] effectPasser = { bossExplosionDamage };
                col.GetComponent<EnemyDamageReceiver>().PassDamage(effectPasser, 1, transform.position);
                continue;
            }
            if (col.CompareTag("Player"))
            {
                //Debug.Log("magnitude" + (location - col.transform.position).magnitude);
                //Debug.Log("dealt: " + (damage * (1 - (location - col.transform.position).magnitude / radius)));
                col.GetComponent<PlayerHealth>().takeDamage(BossManager.instance.boss.DamageCalculation(damage));
                continue;
            }
        }
    }
}
