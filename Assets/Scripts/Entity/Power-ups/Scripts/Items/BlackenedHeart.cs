using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Blackened Heart")]

public class BlackenedHeart : PassiveItem
{
    [Range(0.0f, 0.99f)]
    public float healthVal;
    [Tooltip("Percentage as a decimal ie. writing 0.1 = 10% damage buff")]
    public float damageVal;

    public override void acquire()
    {
        base.acquire();
    }

    public override void effect()
    {
        Inventory.instance.updateStat(Inventory.StatType.health, -healthVal);
        Inventory.instance.updateStat(Inventory.StatType.percentDamageMod, damageVal);
    }
}
