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

    public void Awake()
    {
        instance = this;
    }

    public void SetName(string name)
    {
        bossName = name;
    }

    public void ClearBoss()
    {
        boss = null;
        bossName = "";
    }
}
