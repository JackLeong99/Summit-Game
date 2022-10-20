using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Stat Change only item")]

public class RawStatBuffItem : PassiveItem
{
    public Inventory.StatType type;
    public float val;

    public override void acquire()
    {
        base.acquire();
    }

    public override void effect()
    {
        Inventory.instance.updateStat(type, val);
    }
}
