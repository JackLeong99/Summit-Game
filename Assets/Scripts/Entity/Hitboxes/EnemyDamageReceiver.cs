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
        boss.TakeDamage(dmg, position);
        DamageHandler();
    }

    public void PassDamage(float[] dmg, float tickRate, Vector3 position)
    {
        boss.TakeDamage(dmg, tickRate, position);
        DamageHandler();
    }

    public void DamageHandler()
    {
        Instantiate(onHitParticles, gameObject.transform.position, Quaternion.identity);
        OnDamageTaken?.Invoke();
    }
}
