using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCrystal : ItemBase
{
    public float amount;
    public override void effect(GameObject target)
    {
        target.GetComponent<PlayerStats>().healDamage(amount);
    }
}
