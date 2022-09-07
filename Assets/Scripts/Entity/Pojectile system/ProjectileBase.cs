using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public float maxLifetime;
    public float currentLifetime;
    protected float damage;
    protected List<OnHitEffect> OnHitEffects = new List<OnHitEffect>();

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
        Destroy(gameObject);
    }

    public virtual void OnCollisionEnter(Collision hit) 
    {
        Debug.Log("hit: " + hit);
        Hit();
    }

    public virtual void OnTriggerEnter(Collider other) 
    {
        Debug.Log("hit: " + other);
        if (other.CompareTag("enemyHitbox"))
        {
            Hit();
            other.GetComponent<EnemyDamageReceiver>().PassDamage(damage, transform.position);
            foreach (var OnHit in OnHitEffects)
            {
                Debug.Log("Applied OnHitEffect to: " + other.gameObject);
                OnHit.ApplyOnHitEffects(other.gameObject);
            }
        }
        if (other.CompareTag("Player")) 
        {
            Hit();
            other.GetComponent<PlayerStats>().takeDamage(damage);
            foreach (var OnHit in OnHitEffects)
            {
                Debug.Log("Applied OnHitEffect to: " + other.gameObject);
                OnHit.ApplyOnHitEffects(other.gameObject);
            }
        }
    }

    public void SetDamage(float d) 
    {
        damage = d;
    }

    public void SetOnHitEffect(List<OnHitEffect> effects) 
    {
        OnHitEffects = effects;
    }
}
