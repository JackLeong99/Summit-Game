using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public BossStateMachine boss;
    public DamageTextPool damageTextPool;
    public int killCount;
    public float bossHealthModifier;
    public float bossDamageModifier;



    [Header("Reference")]
    public GameObject portalPrefab;

    public void Awake()
    {
        instance = this;
    }

    public void SetBoss()
    {
        switch (true)
        {
            case bool x when HealthbarManager.instance != null:
                HealthbarManager.instance.SetBoss();
                break;
        }
    }

    public float CalculateHealth()
    {
        return boss.attributes.maxHealth * ( 1 + (bossHealthModifier * killCount));
    }

    public float CalculateDamage()
    {
        return 1 + (bossDamageModifier * killCount);
    }

    public void OnDeath(Vector3 portalPos)
    { 
        ClearBoss(portalPos);
    }

    public void ClearBoss(Vector3 portalPos)
    {
        boss = null;
        HealthbarManager.instance.ClearBoss();

        Instantiate(portalPrefab, portalPos, Quaternion.identity);
    }
}
