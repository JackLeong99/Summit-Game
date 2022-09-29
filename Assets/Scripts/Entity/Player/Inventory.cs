using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using StarterAssets;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> items = new Dictionary<string, int>(); 

    [HideInInspector]
    public ThirdPersonController controller;
    [HideInInspector]
    public PlayerHealth playerHealth;
    [HideInInspector]
    public KnockbackReciever knockback;
    [HideInInspector]
    public PlayerAbilities abilities;

    private float 
        base_SprintSpeed,
        base_WalkSpeed,
        base_Health,
        base_Defense;

    [HideInInspector]
    public float
        walkSpeed,
        sprintSpeed,
        health,
        defense,
        physicalDamage,
        abilityDamage,
        bonusProjectileVelocity,
        knockbackReduction;
    //[HideInInspector]
    public List<OnHitEffect> abilityOnHitEffects = new List<OnHitEffect>();

    private void Start()
    {
        controller = GetComponent<ThirdPersonController>();
        playerHealth = GetComponent<PlayerHealth>();
        knockback = GetComponent<KnockbackReciever>();
        abilities = GetComponent<PlayerAbilities>();

        initializeValues();
    }

    public void initializeValues() 
    {
        base_SprintSpeed = controller.SprintSpeed;
        base_WalkSpeed = controller.MoveSpeed;
        base_Health = playerHealth.maxHealth;
        base_Defense = playerHealth.defence;
    }

    public void resetStats() 
    {
        controller.SprintSpeed = base_SprintSpeed;
        controller.MoveSpeed = base_WalkSpeed;
        playerHealth.maxHealth = base_Health;
        playerHealth.defence = base_Defense;

        physicalDamage = 0;
        abilityDamage = 0;
        bonusProjectileVelocity = 0;
        knockbackReduction = 0;
        abilityOnHitEffects = new List<OnHitEffect>();
    }

    public int GetStacks(string i)
    {
        switch (true)
        {
            case bool x when items.ContainsKey(i):
                return items[i];
            default:
                return 0;
        }
    }
}
