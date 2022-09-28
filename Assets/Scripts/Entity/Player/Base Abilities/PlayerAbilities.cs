using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Ability slots")]
    public ActiveAbility slot1;
    public ActiveAbility slot2;
    public ActiveAbility slot3;
    public ActiveAbility slot4;
    [HideInInspector]
    public List<ActiveAbility> AbilitySlot;
    [Header("List of minimum cooldowns based on the abilities cast time (do not edit, only visible for debugging)")]
    public List<float> internalCooldown;
    [Header("Define the default slot filler (ie an empty ability)")]
    public ActiveAbility defaultAbility;



    private ThirdPersonController playerController;
    private KnockbackReciever knockbackReciever;

    private void Start()
    {
        AbilitySlot = new List<ActiveAbility> { slot1, slot2, slot3, slot4 };
        internalCooldown = new List<float> { 0, 0, 0, 0 };
        playerController = GameManager.instance.player.GetComponent<ThirdPersonController>();
        knockbackReciever = GameManager.instance.player.GetComponent<KnockbackReciever>();
    }

    private void Update()
    {
        for (int i = 0; i < AbilitySlot.Count; i++) 
        {
            internalCooldown[i] -= Time.deltaTime;
            internalCooldown[i] = Mathf.Clamp(internalCooldown[i], 0f, Mathf.Infinity);
        }

        if (playerController._Inactionable || knockbackReciever.impact.magnitude > 5) return;

        if (GameManager.instance.input.meleeAttack && internalCooldown[0] <= 0)
        {
            AbilitySlot[0].effect();
            internalCooldown[0] = AbilitySlot[0].cooldown + AbilitySlot[0].castTime;
            return;
        }
        if (GameManager.instance.input.shoot && internalCooldown[1] <= 0)
        {
            AbilitySlot[1].effect();
            internalCooldown[1] = AbilitySlot[1].cooldown + AbilitySlot[1].castTime;
            return;
        }
        if (GameManager.instance.input.dodge && internalCooldown[2] <= 0)
        {
            AbilitySlot[2].effect();
            internalCooldown[2] = AbilitySlot[2].cooldown + AbilitySlot[2].castTime;
            return;
        }
        if (GameManager.instance.input.activeItem && internalCooldown[3] <= 0)
        {
            AbilitySlot[3].effect();
            internalCooldown[3] = AbilitySlot[3].cooldown + AbilitySlot[3].castTime;
            return;
        }
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
