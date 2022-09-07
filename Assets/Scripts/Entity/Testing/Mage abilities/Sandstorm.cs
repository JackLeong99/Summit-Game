using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm : AreaOfEffect
{
    public float damage;

    public float tickRate;

    public bool covered;

    Coroutine routine;

    public delegate void storming(bool b);
    public static event storming OnStorm;

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
        while(true) 
        {
            yield return new WaitForSeconds(tickRate);
            if(!covered)
            other.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }

    public void toggleDamage(bool b) 
    {
        covered = b;
    }

    public void OnEnable()
    {
        if (OnStorm != null) 
        {
            OnStorm(true);
        }
        sandPillar.OnTrigger += toggleDamage;
    }

    public override void OnDisable()
    {
        if (OnStorm != null)
        {
            OnStorm(false);
        }
        sandPillar.OnTrigger -= toggleDamage;
        StopCoroutine(routine);
    }
}
