using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public BossStateMachine boss;
    public DamageTextPool damageTextPool;
    public string bossName;

    [Header("Reference")]
    public GameObject portalPrefab;

    public void Awake()
    {
        instance = this;
    }

    public void SetName(string name)
    {
        bossName = name;
    }

    public void OnDeath(Vector3 portalPos)
    { 
        ClearBoss(portalPos);
    }

    public void ClearBoss(Vector3 portalPos)
    {
        bossName = "";
        boss = null;

        Instantiate(portalPrefab, portalPos, Quaternion.identity);
    }
}
