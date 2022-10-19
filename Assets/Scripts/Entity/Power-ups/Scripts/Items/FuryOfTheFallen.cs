using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Fury of the fallen")]

public class FuryOfTheFallen : EventItem
{
    public float breakPointPercent;
    public float bonusDamage;

    public enum Breakpoint { inBreakPoint, outBreakPoint }

    public Breakpoint BreakpointState;
    public override void acquire()
    {
        base.acquire();
        BreakpointState = Breakpoint.outBreakPoint;
        PlayerHealth hp = GameManager.instance.player.GetComponent<PlayerHealth>();
        if (hp.currentHealth / hp.maxHealth * 100 <= breakPointPercent)
        {
            Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, bonusDamage);
            BreakpointState = Breakpoint.inBreakPoint;
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
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, bonusDamage);
                BreakpointState = Breakpoint.inBreakPoint;
                break;
            default:
                Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, -bonusDamage);
                BreakpointState = Breakpoint.outBreakPoint;
                break;
        }
    }
}
