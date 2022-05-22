using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private float damage;

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
                receiver.PassDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
