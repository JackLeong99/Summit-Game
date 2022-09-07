using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PowerUpHolder : MonoBehaviour
{
    public Dictionary<ItemBase, float> items; 

    public ThirdPersonController controller;
    public PlayerHealth playerHealth;
    public ThirdPersonShooting gun;
    public AutoAttack attack;
    public Dodge dodge;
    public KnockbackReciever knockback;

    public float runSpeed;
    public float health;
    public float defense;
    public float attackDamage;
    public float projectileSpeed;
    public float projectileDamage;
    public float projectileCooldown;
    public bool projectileGrav;


    private void Start()
    {
        setBases();
    }

    public void setBases() 
    {
        controller = GetComponent<ThirdPersonController>();
        playerHealth = GetComponent<PlayerHealth>();
        gun = GetComponent<ThirdPersonShooting>();
        attack = GetComponent<AutoAttack>();
        dodge = GetComponent<Dodge>();
        knockback = GetComponent<KnockbackReciever>();
    }

    public void ApplyEffects() 
    {

    }
}
