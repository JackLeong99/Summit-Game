using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour
{
    public BossManager boss;
    
    void Start() 
    {
        boss = GetComponentInParent<BossManager>();
        gameObject.tag = "enemyHitbox";
    }
    public void PassDamage(float dmg, Vector3 position) 
    {
        boss.TakeDamage(dmg, position);
    }
}
