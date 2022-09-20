using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    public float damage;
    public Vector2 force;

    private Transform source;


    public void Awake()
    {
        source = transform.root;
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                PlayerHealth health = other.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.takeDamage(damage);
                }

                if (force.x > 0 || force.y > 0)
                {
                    Vector3 dir = other.transform.position - source.transform.position;
                    Knockback(other.gameObject.GetComponent<KnockbackReciever>(), dir);
                }
                break;
        }
    }

    public void Knockback(KnockbackReciever reciever, Vector3 dir)
    {
        switch (force.x)
        {
            case float x when force.x > 0:
                reciever.AddImpactH(dir, force.x);
                break;
        }

        switch (force.y)
        {
            case float y when force.y > 0:
                reciever.AddImpactV(dir, force.y);
                break;
        }
    }
}