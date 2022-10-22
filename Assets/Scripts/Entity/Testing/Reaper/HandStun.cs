using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class HandStun : MonoBehaviour
{
    public List<OnHitEffect> fx;
    private bool hit;
    public float damagePerTick;
    public float tickRate;
    private float tickCounter;
    private PlayerHealth targetHealth;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            hit = true;
            targetHealth = other.GetComponent<PlayerHealth>();
            foreach (var effect in fx) 
            {
                effect.ApplyOnHitEffects(other.gameObject);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            tickCounter += Time.fixedDeltaTime;
            if (tickCounter >= tickRate)
            {
                tickCounter = 0;
                targetHealth.takeDamage(damagePerTick);
            }
        }
    }

    public void OnDisable()
    {
        if(hit) GameManager.instance.player.GetComponent<ThirdPersonController>().stunned = ThirdPersonController.stunState.Actionable;
    }
}
