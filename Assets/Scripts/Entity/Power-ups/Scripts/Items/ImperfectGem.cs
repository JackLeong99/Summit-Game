using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Imperfect Gem")]

public class ImperfectGem : EventItem
{
    public float breakPointPercent;
    public float cooldownReduction;
    public float bonusSpeed;
    public float bonusDamage;

    public enum Breakpoint { inBreakPoint, outBreakPoint }

    public Breakpoint BreakpointState;
    public override void acquire()
    {
        base.acquire();
        PlayerHealth hp = GameManager.instance.player.GetComponent<PlayerHealth>();
        switch (hp.currentHealth / hp.maxHealth * 100 <= breakPointPercent) 
        {
            case true:
                Inventory.instance.updateStat(Inventory.StatType.speed, bonusSpeed);
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, bonusDamage);
                BreakpointState = Breakpoint.inBreakPoint;
                Debug.Log("Breakpoint State " + BreakpointState);
                break;
            default:
                Inventory.instance.updateStat(Inventory.StatType.cooldownReduction, cooldownReduction);
                BreakpointState = Breakpoint.outBreakPoint;
                Debug.Log("Breakpoint State " + BreakpointState);
                break;
        }
    }

    public override void subscribe()
    {
        EventManager.instance.OnHealthChange.AddListener(effect);
    }

    public override void effect()
    {
        throw new System.NotImplementedException();
    }

    public void effect(float f)
    {
        switch (f <= breakPointPercent && BreakpointState == Breakpoint.outBreakPoint) 
        {
            case true:
                Inventory.instance.updateStat(Inventory.StatType.cooldownReduction, -cooldownReduction);
                Inventory.instance.updateStat(Inventory.StatType.speed, bonusSpeed);
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, bonusDamage);
                BreakpointState = Breakpoint.inBreakPoint;
                Debug.Log("Breakpoint State " + BreakpointState);
                break;
            default:
                Inventory.instance.updateStat(Inventory.StatType.cooldownReduction, cooldownReduction);
                Inventory.instance.updateStat(Inventory.StatType.speed, -bonusSpeed);
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, -bonusDamage);
                BreakpointState = Breakpoint.outBreakPoint;
                Debug.Log("Breakpoint State " + BreakpointState);
                break;
        }
    }
}
