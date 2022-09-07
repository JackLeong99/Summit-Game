using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/Burn Player")]

public class PlayerBurn : OnHitEffect
{
    public float damage;

    public float tickRate;

    public float duration;

    public override void ApplyOnHitEffects(GameObject target)
    {
        var player = target.GetComponent<PlayerHealth>();
        if (player) 
        {
            player.StartCoroutine(burn(damage, tickRate, duration, player));
        }
    }

    public IEnumerator burn(float damage, float tickRate, float duration, PlayerHealth stats) 
    {
        float t = duration;
        while (t > 0) 
        {
            stats.takeDamage(damage);
            yield return new WaitForSeconds(tickRate);
            t -= tickRate;
        }
    }

}
