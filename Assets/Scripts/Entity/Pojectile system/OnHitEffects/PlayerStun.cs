using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[CreateAssetMenu(menuName = "OnHitEffects/Stun Player")]

public class PlayerStun : OnHitEffect
{
    public float stunTime;
    public override void ApplyOnHitEffects(GameObject target)
    {
        var player = target.GetComponent<ThirdPersonController>();
        if (player) 
        {
            player.runStun(stunTime);
        }
    }
}
