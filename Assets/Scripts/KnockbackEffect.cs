using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    [SerializeField] float force;

    private Transform dealer;

    private void Awake()
    {
        dealer = transform.root;      
    }

    private void OnTriggerEnter(Collider collision)
    {
        Vector3 dir = collision.transform.position - dealer.transform.position;

        KnockbackReciever reciever = collision.gameObject.GetComponent<KnockbackReciever>();
        if(reciever)
        {
            reciever.AddImpactH(dir, force);
        }
    }
}
