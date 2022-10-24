using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/Gold on Hit")]

public class GoldOnHit : OnHitEffect
{
    public float gain;
    public float luck;
    public ThiefingRodent item;
    public override void ApplyOnHitEffects(GameObject target)
    {
        Inventory inv = Inventory.instance;
        float r = Random.Range(0.0f, 100.0f);
        if (r > (100/(100 + (luck * inv.GetStacks(item)))))
        inv.updateStat(Inventory.StatType.gold, gain);
    }
}
