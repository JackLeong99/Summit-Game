using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class HandStun : MonoBehaviour
{
    public List<OnHitEffect> fx;
    private bool hit;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            hit = true;
            foreach (var effect in fx) 
            {
                effect.ApplyOnHitEffects(other.gameObject);
            }
        }
    }

    public void setFX(List<OnHitEffect> f) 
    {
        fx = f;
    }

    public void OnDisable()
    {
        if(hit) GameManager.instance.player.GetComponent<ThirdPersonController>().stunned = ThirdPersonController.stunState.Actionable;
    }
}
