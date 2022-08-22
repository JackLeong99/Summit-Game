using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/Test DoT for Boss")]

public class BossDoT : OnHitEffect
{
    [Tooltip("Time DoT is applied in seconds")]
    public float time;
    [Tooltip("Number of seconds between damage ticks")]
    public float rate;
    [Tooltip("Damage per tick")]
    public float damage;

    public override void ApplyOnHitEffects(GameObject target)
    {
        target.GetComponent<EnemyDamageReceiver>().StartCoroutine(DoT(target));
    }

    IEnumerator DoT(GameObject target) 
    {
        float currentTime = time;
        EnemyDamageReceiver boss = target.GetComponent<EnemyDamageReceiver>();

        while (currentTime > 0) 
        {
            boss.PassDamage(damage, boss.transform.position);
            currentTime -= rate;
            yield return new WaitForSeconds(rate);
        }

        yield return null;
    }
}
