using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieExplosion : MonoBehaviour
{
    public float lifespan;
    public float lifespanCounter;
    public float explosionDamage;
    private Vector3 temp;

    //public float explosionSize;

    // Start is called before the first frame update
    void Awake()
    {
        //radius is placeholder for now
        DamageFallOff(transform.position, 2.5f, explosionDamage);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifespanCounter >= lifespan)
        {
            Destroy(gameObject);
        }
        lifespanCounter += (Time.deltaTime + .1f);
    }

    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "enemyHitbox")
    //     {
    //         EnemyDamageReceiver receiver = other.GetComponent<EnemyDamageReceiver>();
    //         if (receiver)
    //         {
    //             receiver.PassDamage(explosionDamage, transform.position);
    //         }
    //     }
        
    //     if (other.tag == "hordeEnemy")
    //     {
    //         hordePathing dmgFren = other.GetComponent<hordePathing>();
    //         if(dmgFren)
    //         {
    //             dmgFren.takeDamage(explosionDamage);
    //         }
    //     }

    //     // if (other.tag == "Player")
    //     // {
    //     //     HPTesting testDamage = other.GetComponent<HPTesting>();
    //     //     if(testDamage)
    //     //     {
    //     //         testDamage.takeDamage(explosionDamage);
    //     //     }
    //     // }
    // }

    private void DamageFallOff(Vector3 location,float radius,float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            hordePathing dmgFren = col.GetComponent<hordePathing>();
            if (dmgFren != null)
            {
                // linear falloff of effect
                float proximity = (location - dmgFren.transform.position).magnitude;
                float effect = 1 - (proximity / radius);

                dmgFren.takeDamage(damage * effect);
            
            }
            else
            {
                EnemyDamageReceiver receiver = col.GetComponent<EnemyDamageReceiver>();
                if (receiver != null)
                {
                    // linear falloff of effect
                    float proximity = (location - receiver.transform.position).magnitude;
                    float effect = 1 - (proximity / radius);

                    //position is placeholder
                    receiver.PassDamage(damage * effect, transform.position);
                
                }
                else
                {
                    PlayerHealth health = col.GetComponent<PlayerHealth>();

                    if(health != null)
                    {
                        float proximity = (location - health.transform.position).magnitude;
                        float effect = 1 - (proximity / radius);

                        health.takeDamage(damage * effect);
                    }
                }
            }
        }
    }
}
