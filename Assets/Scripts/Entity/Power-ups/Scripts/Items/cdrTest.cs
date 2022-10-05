using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/testCDR")]

public class cdrTest : PassiveItem
{
    public float cdr;
    public override void acquire()
    {
        base.acquire();
    }

    public override void effect()
    {
        GameManager.instance.player.GetComponent<Inventory>().updateStat(Inventory.StatType.cooldownReduction, cdr);
    }
}
