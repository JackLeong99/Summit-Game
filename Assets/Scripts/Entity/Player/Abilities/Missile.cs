using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : ProjectileBase
{
    public float vel;
    public float tracking;
    public GameObject target;
    private Rigidbody rb;

    public void Start()
    {
        Physics.IgnoreLayerCollision(9, 9);
        rb = gameObject.GetComponent<Rigidbody>();
        //Physics.IgnoreLayerCollision(3, 9);
    }
    public override void Update()
    {
        base.Update();
        if (target != null)
        {
            transform.rotation = Quaternion.LookRotation
            (
                Vector3.RotateTowards
                (
                    transform.forward,
                    (target.transform.position - transform.position).normalized, tracking * Mathf.Deg2Rad * Time.deltaTime, 1f
                )
            );
            rb.velocity = transform.forward * vel;
        }
    }

    public override void Hit()
    {
        base.Hit();
    }

    public override void OnCollisionEnter(Collision hit)
    {
        base.OnCollisionEnter(hit);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public void setTracking(float tr, float de, float ve, GameObject ta) 
    {
        tracking = 0;
        target = ta;
        vel = ve;
        StartCoroutine(track(de, tr));
    }

    private IEnumerator track(float d, float t) 
    {
        yield return new WaitForSeconds(d);
        tracking = t;
    }
}
