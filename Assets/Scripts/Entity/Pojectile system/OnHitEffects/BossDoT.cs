using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/Test DoT for Boss")]

public class BossDoT : OnHitEffect
{
    [Tooltip("Number of seconds between damage ticks")]
    public float rate;
    [Tooltip("Damage per tick")]
    public float[] damage;

    public override void ApplyOnHitEffects(GameObject target)
    {
        target.GetComponent<EnemyDamageReceiver>().PassDamage(damage, rate, target.transform.position);
    }

}
