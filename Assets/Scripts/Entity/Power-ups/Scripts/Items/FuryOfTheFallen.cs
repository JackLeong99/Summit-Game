using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Fury of the fallen")]

public class FuryOfTheFallen : EventItem
{
    public float breakPointPercent;
    public float bonusDamage;
    private Inventory inv;

    public enum Breakpoint { inBreakPoint, outBreakPoint }

    public Breakpoint BreakpointState;
    public override void acquire()
    {
        base.acquire();
    }

    public override void subscribe()
    {
        EventManager.instance.OnHealthChange.AddListener(effect);
        BreakpointState = Breakpoint.outBreakPoint;
        PlayerHealth hp = GameManager.instance.player.GetComponent<PlayerHealth>();
        inv = GameManager.instance.player.GetComponent<Inventory>();
        if (hp.currentHealth / hp.maxHealth * 100 <= breakPointPercent)
        {
            inv.updateStat(Inventory.StatType.percentDamageMod, bonusDamage * inv.GetStacks(this));
            BreakpointState = Breakpoint.inBreakPoint;
        }
    }

    public override void effect()
    {
        throw new System.NotImplementedException();
    }

    public void effect(float f)
    {
        inv = GameManager.instance.player.GetComponent<Inventory>();
        switch (BreakpointState)
        {
            case Breakpoint.outBreakPoint:
                if (f >= breakPointPercent)
                    break;
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, bonusDamage * inv.GetStacks(this));
                BreakpointState = Breakpoint.inBreakPoint;
                break;
            case Breakpoint.inBreakPoint:
                if (f < breakPointPercent)
                    break;
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, -bonusDamage * inv.GetStacks(this));
                BreakpointState = Breakpoint.outBreakPoint;
                break;
        }
    }
}
