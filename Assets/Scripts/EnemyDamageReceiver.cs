using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour
{
    public BossManager boss;
    public GameObject onHitParticles;
    
    void Start() 
    {
        boss = GetComponentInParent<BossManager>();
        gameObject.tag = "enemyHitbox";
    }
    public void PassDamage(float dmg, Vector3 position) 
    {
        Instantiate(onHitParticles, gameObject.transform.position, Quaternion.identity);
        boss.TakeDamage(dmg, position);
    }
}
