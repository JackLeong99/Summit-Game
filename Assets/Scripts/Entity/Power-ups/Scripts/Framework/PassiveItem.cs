using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveItem : ItemBase
{
    public override void acquire()
    {
        Debug.Log("Acquired: " + this.itemName);
        Inventory inv = GameManager.instance.player.GetComponent<Inventory>();
        switch (true) 
        {
            case bool x when inv.GetStacks(this) == 0:
                inv.items.Add(this, 1);
                effect();
                break;
            default:
                inv.items[this] += 1;
                break;
        }
    }
}
