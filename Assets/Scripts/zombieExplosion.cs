using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieExplosion : MonoBehaviour
{
    public float lifespan;
    public float lifespanCounter;
    public float explosionDamage;

    //public float explosionSize;

    // Start is called before the first frame update
    void Awake()
    {
        
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyHitbox")
        {
            EnemyDamageReceiver receiver = other.GetComponent<EnemyDamageReceiver>();
            if (receiver)
            {
                receiver.PassDamage(explosionDamage, transform.position);
            }
        }
        
        if (other.tag == "hordeEnemy")
        {
            hordePathing dmgFren = other.GetComponent<hordePathing>();
            if(dmgFren)
            {
                dmgFren.takeDamage(explosionDamage);
            }
        }

        // if (other.tag == "Player")
        // {
        //     HPTesting testDamage = other.GetComponent<HPTesting>();
        //     if(testDamage)
        //     {
        //         testDamage.takeDamage(explosionDamage);
        //     }
        // }
    }
}
