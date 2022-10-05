using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/KeyStoryItem")]

public class KeyItem : PassiveItem
{
    public override void effect()
    {
        throw new System.NotImplementedException();
    }

    public override void acquire()
    {
        Inventory inv = GameManager.instance.player.GetComponent<Inventory>();
        inv.keyItem = this;
    }
}
