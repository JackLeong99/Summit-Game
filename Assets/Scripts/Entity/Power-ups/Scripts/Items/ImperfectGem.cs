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
    private Inventory inv;

    public enum Breakpoint { inBreakPoint, outBreakPoint }

    public Breakpoint BreakpointState;
    public override void acquire()
    {
        base.acquire();
        PlayerHealth hp = GameManager.instance.player.GetComponent<PlayerHealth>();
        inv = GameManager.instance.player.GetComponent<Inventory>();
        int s = inv.GetStacks(this);
        switch (hp.currentHealth / hp.maxHealth * 100 < breakPointPercent) 
        {
            case true:
                Inventory.instance.updateStat(Inventory.StatType.speed, bonusSpeed * s);
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, bonusDamage * s);
                BreakpointState = Breakpoint.inBreakPoint;
                break;
            default:
                Inventory.instance.updateStat(Inventory.StatType.cooldownReduction, cooldownReduction * s);
                BreakpointState = Breakpoint.outBreakPoint;
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
        int s = inv.GetStacks(this);
        switch (BreakpointState) 
        {
            case Breakpoint.outBreakPoint:
                if (f >= breakPointPercent)
                    break;
                Inventory.instance.updateStat(Inventory.StatType.cooldownReduction, -cooldownReduction * s);
                Inventory.instance.updateStat(Inventory.StatType.speed, bonusSpeed * s);
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, bonusDamage * s);
                BreakpointState = Breakpoint.inBreakPoint;
                break;
            case Breakpoint.inBreakPoint:
                if (f < breakPointPercent)
                    break;
                Inventory.instance.updateStat(Inventory.StatType.cooldownReduction, cooldownReduction * s);
                Inventory.instance.updateStat(Inventory.StatType.speed, -bonusSpeed * s);
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, -bonusDamage * s);
                BreakpointState = Breakpoint.outBreakPoint;
                break;
        }
    }
}
