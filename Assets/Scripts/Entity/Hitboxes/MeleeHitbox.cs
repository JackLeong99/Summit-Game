using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    public bool dontUseRoot;
    public float hitLockout;
    public float damage;
    public Vector2 force;

    private Transform source;


    public void Awake()
    {
        switch (dontUseRoot) 
        {
            case true:
                source = gameObject.transform;
                break;
            default:
                source = transform.root;
                break;
        }
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

                if (hitLockout > 0) 
                {
                    StartCoroutine(lockout(hitLockout));
                }

                if (force.x + force.y > 0)
                {
                    Vector3 dir = (other.transform.position - source.transform.position);
                    Debug.Log("dir: " + dir);
                    Knockback(other.gameObject.GetComponent<KnockbackReciever>(), dir);
                }
                break;
        }
    }

    public IEnumerator lockout(float f) 
    {
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(f);
        gameObject.GetComponent<Collider>().enabled = true;
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