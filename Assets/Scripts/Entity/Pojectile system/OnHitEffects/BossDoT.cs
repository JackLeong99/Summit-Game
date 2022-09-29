using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/Test DoT for Boss")]

public class BossDoT : OnHitEffect
{
    [Tooltip("Number of seconds between damage ticks")]
    public float rate;
    [Tooltip("Damage per tick")]
    public float damage;
    public float numberOfTicks;
    [Tooltip("Corresponding powerup")]
    public string item;

    private float[] damageStack;
    private Inventory player;

    public override void ApplyOnHitEffects(GameObject target)
    {
        player = GameManager.instance.player.GetComponent<Inventory>();
        for (int i = 0; i < numberOfTicks; i++) 
        {
            damageStack[i] = damage + (1 * player.GetStacks(item));
        }
        target.GetComponent<EnemyDamageReceiver>().PassDamage(damageStack, rate, target.transform.position);
    }

}
