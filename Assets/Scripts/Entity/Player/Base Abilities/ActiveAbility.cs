using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : ItemBase
{
    public int slot;
    public float cooldown;
    [HideInInspector]
    public float castTime;
    public abstract IEnumerator doEffect();

    public override void acquire()
    {
        Debug.Log("Acquired: " + this.itemName);
        PlayerAbilities playerAbilities = GameManager.instance.player.GetComponent<PlayerAbilities>();
        playerAbilities.AbilitySlot[slot] = this;
        UIItemDisplay.instance.ActiveItemSet();
    }
}
