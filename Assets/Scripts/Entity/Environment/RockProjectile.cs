using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : ProjectileBase
{
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private ParticleSystem rockParticle;
    private Rigidbody rb;
    public float groundPosY = -702.226f;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Hit()
    {
        base.Hit();
    }

    public override void OnCollisionEnter(Collision hit)
    {
        switch (hit.gameObject.tag) 
        {
            case "Arena":
                AkSoundEngine.PostEvent("Enemy_Boulder_Impact", gameObject);
                spawnRock();
                break;
            case "Barrier":
                rb.useGravity = true;
                rb.velocity *= 0.2f;
                break;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        switch (other.tag) 
        {
            case "Player":
                other.GetComponent<PlayerHealth>().takeDamage(BossManager.instance.boss.DamageCalculation(damage));
                foreach (var OnHit in OnHitEffects)
                {
                    OnHit.ApplyOnHitEffects(other.gameObject);
                }
                break;
            case "worldBorder":
                spawnRock();
                break;
            default:
                break;
        }
    }

    public void spawnRock()
    {
        GameObject breakablerock = Instantiate(rockPrefab, new Vector3(transform.position.x, groundPosY, transform.position.z), Quaternion.identity);
        ParticleSystem rockParticles = Instantiate(rockParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
