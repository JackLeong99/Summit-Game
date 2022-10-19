using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/FleshSiphon OnHitEffect")]

public class SiphonOnHit : OnHitEffect
{
    public float gain;
    public FleshSiphon item;
    public override void ApplyOnHitEffects(GameObject target)
    {
        Inventory.instance.updateStat(Inventory.StatType.health, gain * Inventory.instance.GetStacks(item));
    }
}
