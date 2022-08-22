using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/BossStagger")]

public class BossStagger : OnHitEffect
{
    public override void ApplyOnHitEffects(GameObject target)
    {
        var boss = target.GetComponentInParent<BossStateMachine>();
        if (boss) 
        {
            //Debug.Log("Stunned Boss!");
            boss.components.stunState = StunState.Stunned;
        }
    }
}
