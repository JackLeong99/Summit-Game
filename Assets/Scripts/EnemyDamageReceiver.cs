using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour
{
    public BossStateMachine boss;
    public GameObject onHitParticles;
    public static event Action OnDamageTaken;
    
    void Start() 
    {
        boss = GetComponentInParent<BossStateMachine>();
        gameObject.tag = "enemyHitbox";
    }
    public void PassDamage(float dmg, Vector3 position) 
    {
        Instantiate(onHitParticles, gameObject.transform.position, Quaternion.identity);
        boss.TakeDamage(dmg, position);
        OnDamageTaken?.Invoke();
    }
}
