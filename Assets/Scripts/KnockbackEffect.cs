using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float force;
    private void OnTriggerEnter(Collider collision)
    {
        Vector3 dir = collision.transform.position - transform.position;

        KnockbackReciever reciever = collision.gameObject.GetComponent<KnockbackReciever>();
        if(reciever)
        {
            reciever.AddImpactH(dir, force);
        }
    }
}
