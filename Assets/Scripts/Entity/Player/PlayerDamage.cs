using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float damage;

    public void setDamage(float dmg) 
    {
        damage = dmg;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyHitbox") 
        {
            EnemyDamageReceiver receiver = other.GetComponent<EnemyDamageReceiver>();
            if (receiver)
            {
                receiver.PassDamage(damage, transform.position);
                Destroy(gameObject);
            }
        }
    }

    // public void percentIncreaseDamage(float dmg)
    // {
    //     damage += (damage/100) * dmg;
    // }

    // public void percentDecreasedamage(float dmg)
    // {
    //     damage -= (damage/100) * dmg;
    // }
}
