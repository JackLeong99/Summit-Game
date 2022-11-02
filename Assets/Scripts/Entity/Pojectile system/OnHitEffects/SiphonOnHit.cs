using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/FleshSiphon OnHitEffect")]

public class SiphonOnHit : OnHitEffect
{
    public float gain;
    public FleshSiphon item;
    private float currentFightGain;
    public float perFightCap;
    private float currentKills = 0;
    public override void ApplyOnHitEffects(GameObject target)
    {
        //hacky solution but it works. An on bosskill event would make this much neater.
        if (currentKills != BossManager.instance.killCount)
        {
            currentKills = BossManager.instance.killCount;
            currentFightGain = 0;
        }
        if (currentFightGain < perFightCap)
        {
            Inventory.instance.updateStat(Inventory.StatType.health, gain * Inventory.instance.GetStacks(item));
            currentFightGain += gain;
        }
    }
}
