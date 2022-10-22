using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Starting Abilities")]
    public ActiveAbility slot1;
    public ActiveAbility slot2;
    public ActiveAbility slot3;
    public ActiveAbility slot4;
    [Header("Current Abilities")]
    public List<ActiveAbility> AbilitySlot;
    [Header("List of minimum cooldowns based on the abilities cast time (do not edit, only visible for debugging)")]
    public List<float> internalCooldown;
    [Header("Cooldown reduction (0 - 100%)")]
    public float cooldownReduction;
    public float cdrMod;
    [Header("Minimum possible cooldown")]
    public float minCD;
    [Header("Cooldown reduction cap")]
    public float maxCDR;
    [Header("Define the default slot filler (ie an empty ability)")]
    public ActiveAbility defaultAbility;
    public ThirdPersonController controller;

    public enum Lockout { Unlocked, Locked }
    public Lockout abilityLockout;

    private void Start()
    {
        controller = GetComponent<ThirdPersonController>();
        AbilitySlot = new List<ActiveAbility> { slot1, slot2, slot3, slot4 };
        internalCooldown = new List<float> { 0, 0, 0, 0 };
        addCDR(cooldownReduction);
    }

    private void Update()
    {
        for (int i = 0; i < AbilitySlot.Count; i++) 
        {
            internalCooldown[i] -= Time.deltaTime;
            internalCooldown[i] = Mathf.Clamp(internalCooldown[i], 0f, Mathf.Infinity);
        }

        if (abilityLockout == Lockout.Locked || controller.stunned == ThirdPersonController.stunState.Stunned) return;

        if (GameManager.instance.input.meleeAttack && internalCooldown[0] <= 0)
        {
            AbilitySlot[0].effect();
            internalCooldown[0] = AbilitySlot[0].cooldown + AbilitySlot[0].castTime;
            //internalCooldown[0] = Mathf.Clamp((AbilitySlot[0].cooldown * cdrMod) + AbilitySlot[0].castTime, minCD, Mathf.Infinity);
            return;
        }
        if (GameManager.instance.input.shoot && internalCooldown[1] <= 0)
        {
            AbilitySlot[1].effect();
            internalCooldown[1] = Mathf.Clamp((AbilitySlot[1].cooldown * cdrMod) + AbilitySlot[1].castTime, minCD, AbilitySlot[1].cooldown + AbilitySlot[1].castTime);
            return;
        }
        if (GameManager.instance.input.dodge && internalCooldown[2] <= 0)
        {
            AbilitySlot[2].effect();
            internalCooldown[2] = Mathf.Clamp((AbilitySlot[2].cooldown * cdrMod) + AbilitySlot[2].castTime, minCD, AbilitySlot[2].cooldown + AbilitySlot[2].castTime);
            return;
        }
        if (GameManager.instance.input.activeItem && internalCooldown[3] <= 0)
        {
            AbilitySlot[3].effect();
            internalCooldown[3] = Mathf.Clamp((AbilitySlot[3].cooldown * cdrMod) + AbilitySlot[3].castTime, minCD, AbilitySlot[3].cooldown + AbilitySlot[3].castTime);
            return;
        }
    }

    public void addCDR(float c) 
    {
        cooldownReduction = Mathf.Clamp(cooldownReduction += c, 0f, Mathf.Infinity);
        float clampedCDR = Mathf.Clamp(cooldownReduction, 0f, maxCDR);
        cdrMod = ((100 - clampedCDR) / 100);
    }

    public void resetCDR() 
    {
        cooldownReduction = 0;
        cdrMod = 1;
    }

    public void fillSlots(ActiveAbility def) 
    {
        //this function is a safety net to be called if an ability is to be removed but not replaced with another
        int i = 0;
        foreach (ActiveAbility a in AbilitySlot) 
        {
            if (a == null) 
            {
               AbilitySlot[i] = def;
               i++;
            }
        }
    }
}
