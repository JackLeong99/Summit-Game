using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/testDoT")]
public class UtilGun : ItemBase
{
    public OnHitEffect hitEffect;
    public override void effect()
    {
        Inventory inv = GameManager.instance.player.GetComponent<Inventory>();
        Debug.Log("utilgun name: " + itemName);
        if (inv.GetStacks(this) == 0)
        {
            inv.items.Add(this, 1);
            inv.abilityOnHitEffects.Add(hitEffect);
        }
        else 
        {
            inv.items[this] += 1;
        }
    }
}
