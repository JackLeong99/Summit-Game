using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Giants Will")]

public class GiantsWill : PassiveItem
{
    public float healthVal;
    public float damageVal;
    public float speedVal;

    public override void acquire()
    {
        base.acquire();
    }

    public override void effect()
    {
        Inventory.instance.updateStat(Inventory.StatType.health, healthVal);
        Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, -damageVal);
        Inventory.instance.updateStat(Inventory.StatType.speed, -speedVal);
    }
}
