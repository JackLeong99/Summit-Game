using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/Gold on Hit")]

public class GoldOnHit : OnHitEffect
{
    public float gain;
    public float ratio;
    public ThiefingRodent item;
    public override void ApplyOnHitEffects(GameObject target)
    {
        Inventory.instance.updateStat(Inventory.StatType.gold, gain * (Inventory.instance.GetStacks(item)/ratio));
    }
}
