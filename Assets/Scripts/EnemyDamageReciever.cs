using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour
{
    public BossManager boss;
    
    void Start() 
    {
        boss = GetComponentInParent<BossManager>();
    }
    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "playerHit")
        {
            PlayerDamage playerDamage = collider.GetComponent<PlayerDamage>();
            if(playerDamage)
            {
                boss.TakeDamage(playerDamage.damage);
            }
        }
    }
}
