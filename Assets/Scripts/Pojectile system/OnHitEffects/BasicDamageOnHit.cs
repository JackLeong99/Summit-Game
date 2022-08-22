using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OnHitEffects/BasicDamageOnHit")]

public class BasicDamageOnHit : OnHitEffect
{
    public float damage;
    public override void ApplyOnHitEffects(GameObject target)
    {
        target.GetComponent<EnemyDamageReceiver>().PassDamage(damage, target.transform.position);
        if (target.GetComponent<EnemyDamageReceiver>()) 
        {
            Debug.Log("They have a damage reciever!");
        }
    }
}
