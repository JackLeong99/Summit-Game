using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/New Attributes")]
[Serializable] public class BossAttributes : ScriptableObject
{
    [Header("Distance Checks")]
    public int minPlayerDist;
    public int maxPlayerDist;
    public float turnSpeed;
    public float turnFor = 1f;

    [Header("Attacks")]
    public float attackCooldown;
    public float rangedCooldown;
    public float patience;

    [Header("Health")]
    public float maxHealth;
    public float iFrameTime;

    [Header("Rage")]
    public float rageSpeed;
    public float rageMultiplier;

    [Header("Stun")]
    public float stunTime;
}
