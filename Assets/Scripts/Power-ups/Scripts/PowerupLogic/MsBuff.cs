using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/MovementSpeedBuff")]
public class MsBuff : ItemBase
{
    public int amount;
    public override void effect(GameObject target)
    {
        target.GetComponent<ItemFrame>().msStacks += amount;
    }
}
