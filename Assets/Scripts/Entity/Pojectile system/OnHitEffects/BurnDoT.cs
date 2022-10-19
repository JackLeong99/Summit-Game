using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/Burn (applied to enemies)")]

public class BurnDoT : OnHitEffect
{
    [Tooltip("Number of seconds between damage ticks")]
    public float rate;
    [Tooltip("Damage per tick")]
    public float damage;
    public int numberOfTicks;
    [Tooltip("Corresponding powerup")]
    public CrudeOil item;

    private float[] damageStack;

    public override void ApplyOnHitEffects(GameObject target)
    {
        damageStack = new float[numberOfTicks];
        float damagePerTick = damage + (damage * Inventory.instance.GetStacks(item));
        for (int i = 0; i < numberOfTicks; i++) 
        {
            damageStack[i] = damagePerTick;
        }
        target.GetComponent<EnemyDamageReceiver>().PassDamage(damageStack, rate, target.transform.position);
    }

}
