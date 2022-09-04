using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm : AreaOfEffect
{
    public float damage;

    public float tickRate;

    PlayerStats player;

    Coroutine routine;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("player enter");
            player = other.GetComponent<PlayerStats>();
            routine = StartCoroutine(doEffect());
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("player exit");
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

    public void OnDisable()
    {
        StopCoroutine(routine);
    }
}
