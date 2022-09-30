using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/testDoT")]
public class UtilGun : PassiveItem
{
    public OnHitEffect hitEffect;
    public override void acquire()
    {
        base.acquire();
    }

    public override void effect()
    {
        Inventory inv = GameManager.instance.player.GetComponent<Inventory>();
        inv.abilityOnHitEffects.Add(hitEffect);
    }
}
