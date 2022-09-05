using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm : AreaOfEffect
{
    public float damage;

    public float tickRate;

    public bool covered;

    PlayerStats player;

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
        while(true) 
        {
            yield return new WaitForSeconds(tickRate);
            if(!covered)
            other.gameObject.GetComponent<PlayerStats>().takeDamage(damage);
        }
    }

    public void toggleDamage() 
    {
        covered = !covered;
    }

    public void OnEnable()
    {
        sandPillar.OnTrigger += toggleDamage;
    }

    public override void OnDisable()
    {
        sandPillar.OnTrigger -= toggleDamage;
        StopCoroutine(routine);
    }
}
