using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockUpEffect : MonoBehaviour
{
    [SerializeField] float force;
    private void OnTriggerEnter(Collider collision)
    {
        Vector3 dir = collision.transform.position - transform.position;

        KnockbackReciever reciever = collision.gameObject.GetComponent<KnockbackReciever>();
        if(reciever)
        {
            reciever.AddImpactV(dir, force);
        }
    }
}
