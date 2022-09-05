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
        if (other.CompareTag("Player")) 
        {
            player = other.GetComponent<PlayerStats>();
            routine = StartCoroutine(doEffect());
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            //player = other.GetComponent<PlayerStats>();
            StopCoroutine(routine);
        }
    }

    public IEnumerator doEffect()
    {
        while(true) 
        {
            yield return new WaitForSeconds(tickRate);
            player.takeDamage(damage);
        }
    }

    public void toggleDamage() 
    {
        switch (covered) 
        {
            case true:
                routine = StartCoroutine(doEffect());
                covered = false;
                break;
            case false:
                StopCoroutine(routine);
                covered = true;
                break;
        }

    }

    public void OnEnable()
    {
        sandPillar.OnTrigger += toggleDamage;
    }

    public void OnDisable()
    {
        sandPillar.OnTrigger -= toggleDamage;
        StopCoroutine(routine);
    }
}
