using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Crude Oil")]
public class CrudeOil : PassiveItem
{
    public OnHitEffect hitEffect;
    public override void acquire()
    {
        base.acquire();
    }

    public override void effect()
    {
        if(Inventory.instance.GetStacks(this) == 0)
            Inventory.instance.abilityOnHitEffects.Add(hitEffect);
    }
}
