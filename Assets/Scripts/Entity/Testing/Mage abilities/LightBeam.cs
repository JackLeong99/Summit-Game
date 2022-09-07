using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : AreaOfEffect
{
    public float damage;

    public float tickRate;

    Coroutine routine;
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    public override IEnumerator doEffect(Collider other)
    {
        while (true) 
        {
            yield return new WaitForSeconds(tickRate);
            other.gameObject.GetComponent<PlayerStats>().takeDamage(damage);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public void setDamage(float d, float t) 
    {
        damage = d;
        tickRate = t;
    }
}
