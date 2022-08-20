using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public float maxLifetime;
    private float currentLifetime;
    protected OnHitEffect OnHitEffect;
    public ParticleSystem hitFX;


    public virtual void Update()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Hit()
    {
        hitFX.Play();
        Destroy(gameObject);
    }

    public virtual void OnCollisionEnter(Collision hit) 
    {
        Hit();
    }

    public virtual void OnTriggerEnter(Collider other) 
    {
        Hit();
        OnHitEffect.ApplyOnHitEffects(other.gameObject);
    }

    public void SetOnHitEffect(OnHitEffect effect) 
    {
        OnHitEffect = effect;
    }
}
