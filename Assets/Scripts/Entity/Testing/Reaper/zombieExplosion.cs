using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieExplosion : MonoBehaviour
{
    public float lifespan;
    public float lifespanCounter;
    public float explosionDamage;
    public float explosionRadius;
    private Vector3 temp;
    

    //public float explosionSize;

    // Start is called before the first frame update
    void Awake()
    {
        //radius is placeholder for now
        DamageFallOff(transform.position, explosionRadius, explosionDamage);
        
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
                float effect = Mathf.Clamp(1 - (proximity / radius), 1, explosionDamage);

                dmgFren.takeDamage(damage * effect);
            }
            else
            {
                EnemyDamageReceiver receiver = col.GetComponent<EnemyDamageReceiver>();
                if (receiver != null)
                {
                    // linear falloff of effect
                    float proximity = (location - receiver.transform.position).magnitude;
                    //gives magnitude / radius - percentage ratio.

                    //This method gives = explosion damage every time.
                    //float effect = Mathf.Clamp(1 - (proximity / radius), 1, explosionDamage);
                    
                    
                    //This method works but causes a lag spike.
                    float effect = 1 - (proximity / radius);
                    if(effect < 0)
                    {
                        effect *= -1;
                    }

                    //position is placeholder
                    float[] effectPasser = new float[1];
                    effectPasser[0] = Mathf.Round((damage * effect) * BossManager.instance.SetBossDamage());
                    receiver.PassDamage(effectPasser, 1, transform.position);
                }
                else
                {
                    PlayerHealth health = col.GetComponent<PlayerHealth>();

                    if(health != null)
                    {
                        float proximity = (location - health.transform.position).magnitude;
                        float effect = Mathf.Clamp(1 - (proximity / radius), 1, explosionDamage);

                        health.takeDamage((damage * effect) * BossManager.instance.SetBossDamage());
                    }
                }
            }
        }
    }
}
