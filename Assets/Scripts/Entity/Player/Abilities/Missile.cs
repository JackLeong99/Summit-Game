using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : ProjectileBase
{
    public float tracking;
    public GameObject target;

    public void Start()
    {
        Physics.IgnoreLayerCollision(9, 9);
        Physics.IgnoreLayerCollision(3, 9);
    }
    public override void Update()
    {
        base.Update();
        if(target!= null)
        gameObject.GetComponent<Rigidbody>().velocity += (target.transform.position - gameObject.transform.position).normalized * tracking;
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

    public void setTracking(float tr, GameObject ta) 
    {
        tracking = tr;
        target = ta;
    }
}
