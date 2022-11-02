using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float damage;
    private Inventory inv;

    public void Start()
    {
        inv = Inventory.instance;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyHitbox") 
        {
            EnemyDamageReceiver receiver = other.GetComponent<EnemyDamageReceiver>();
            receiver.PassDamage((damage + inv.physicalDamage) * inv.percentDamageMod, transform.position, false);
        }
    }
}
