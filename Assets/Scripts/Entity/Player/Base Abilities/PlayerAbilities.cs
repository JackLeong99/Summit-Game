using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Ability slot 1")]
    public ActiveAbility slot1;
    public float cooldown1;

    [Header("Ability slot 2")]
    public ActiveAbility slot2;
    public float cooldown2;

    [Header("Ability slot 3")]
    public ActiveAbility slot3;
    public float cooldown3;

    [Header("Active item slot")]
    public ActiveAbility slotItem;
    public float cooldownItem;

    private ThirdPersonController playerController;
    private KnockbackReciever knockbackReciever;

    private void Start()
    {
        playerController = GameManager.instance.player.GetComponent<ThirdPersonController>();
        knockbackReciever = GameManager.instance.player.GetComponent<KnockbackReciever>();
    }

    private void Update()
    { 
        cooldown1 -= Time.deltaTime;
        cooldown1 = Mathf.Clamp(cooldown1, 0f, Mathf.Infinity);
        cooldown2 -= Time.deltaTime;
        cooldown2 = Mathf.Clamp(cooldown2, 0f, Mathf.Infinity);
        cooldown3 -= Time.deltaTime;
        cooldown3 = Mathf.Clamp(cooldown3, 0f, Mathf.Infinity);
        cooldownItem -= Time.deltaTime;
        cooldownItem = Mathf.Clamp(cooldownItem, 0f, Mathf.Infinity);

        if (playerController._Inactionable || knockbackReciever.impact.magnitude > 5) return;

        if (GameManager.instance.input.meleeAttack && cooldown1 <= 0)
        {
            switch (true)
            {
                case bool x when slot1 == null:
                    break;
                default:
                    slot1.effect();
                    cooldown1 = slot1.cooldown;
                    break;
            }
            return;
        }
        if (GameManager.instance.input.shoot && cooldown2 <= 0)
        {
            switch (true)
            {
                case bool x when slot2 == null:
                    break;
                default:
                    slot2.effect();
                    cooldown2 = slot2.cooldown;
                    break;
            }
            return;
        }
        if (GameManager.instance.input.dodge && cooldown3 <= 0)
        {
            switch (true)
            {
                case bool x when slot3 == null:
                    break;
                default:
                    slot3.effect();
                    cooldown3 = slot3.cooldown;
                    break;
            }
            return;
        }
        if (GameManager.instance.input.activeItem && cooldownItem <= 0)
        {
            switch (true)
            {
                case bool x when slotItem == null:
                    break;
                default:
                    slot1.effect();
                    cooldownItem = slotItem.cooldown;
                    break;
            }
        }
    }
}
