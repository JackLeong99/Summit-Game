using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using StarterAssets;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Dictionary<ItemBase, int> items = new Dictionary<ItemBase, int>(); 

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
    public List<ActiveAbility> base_ActiveAbilities = new List<ActiveAbility>();

    //[HideInInspector]
    public float
        walkSpeed,
        sprintSpeed,
        health,
        defense,
        physicalDamage,
        abilityDamage,
        percentDamageMod,
        bonusProjectileVelocity,
        knockbackReduction,
        cooldownReduction;

    public enum StatType 
    {
        speed,
        health,
        defense,
        physicalDamage,
        abilityDamage,
        percentDamageMod,
        bonusProjectileVelocity,
        knockbackReduction,
        cooldownReduction,
        gold
    }

    //[HideInInspector]
    public List<OnHitEffect> abilityOnHitEffects = new List<OnHitEffect>();
    //TODO on attack effects ( less powerful onHitEffects for more frequent attacks ie. basic attack)
    //public List<OnHitEffect> OnAttackEffects = new List<OnHitEffects>();
    public int gold;
    public PassiveItem keyItem;

    private void Awake()
    {
        instance = this;
    }

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
        base_ActiveAbilities = abilities.AbilitySlot;
        percentDamageMod = 1.0f;
    }

    public void resetStats() 
    {
        controller.SprintSpeed = base_SprintSpeed;
        controller.MoveSpeed = base_WalkSpeed;
        playerHealth.maxHealth = base_Health;
        playerHealth.defence = base_Defense;

        physicalDamage = 0;
        abilityDamage = 0;
        percentDamageMod = 1.0f;
        bonusProjectileVelocity = 0;
        knockbackReduction = 0;
        cooldownReduction = 0;
        abilityOnHitEffects = new List<OnHitEffect>();
        abilities.AbilitySlot = base_ActiveAbilities;
    }

    public int GetStacks(ItemBase i)
    {
        switch (true)
        {
            case bool x when items.ContainsKey(i):
                return items[i];
            default:
                return 0;
        }
    }

    public void updateStat(StatType type, float val) 
    {
        switch (type) 
        {
            case StatType.speed:
                walkSpeed += val;
                controller.MoveSpeed = Mathf.Clamp(base_WalkSpeed + walkSpeed, 1.0f, Mathf.Infinity);
                controller.SprintSpeed = Mathf.Clamp(base_SprintSpeed + sprintSpeed, 1.0f, Mathf.Infinity);
                break;
            case StatType.health:
                health += val;
                playerHealth.maxHealth = Mathf.Clamp(base_Health + health, 1.0f, Mathf.Infinity);
                playerHealth.healDamage(0);
                break;
            case StatType.defense:
                defense += val;
                playerHealth.defence = base_Defense + defense;
                break;
            case StatType.physicalDamage:
                physicalDamage += val;
                break;
            case StatType.abilityDamage:
                abilityDamage += val;
                break;
            case StatType.percentDamageMod:
                percentDamageMod += val;
                break;
            case StatType.bonusProjectileVelocity:
                bonusProjectileVelocity += val;
                break;
            case StatType.knockbackReduction:
                knockbackReduction += val;
                //TODO knockbackreduction
                break;
            case StatType.cooldownReduction:
                cooldownReduction += val;
                abilities.addCDR(val);
                break;
            case StatType.gold:
                gold += Mathf.RoundToInt(val);
                break;
        }
        //Debug.Log("Updated: " + type + ", by: " + val);
    }

    public bool CanPurchase(int cost)
    {
        bool temp = gold >= cost;

        if (temp)
        {
            gold -= cost;
        }

        UIItemDisplay.instance.UpdateGold();

        return temp;
    }
}
