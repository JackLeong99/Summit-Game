using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStun : MonoBehaviour
{
    public List<OnHitEffect> fx;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
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
}
