using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaOfEffect : MonoBehaviour
{
    public string hitTag;

    Coroutine routine;
    public virtual void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(hitTag))
        {
            routine = StartCoroutine(doEffect(other));
        }
    }

    public virtual void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag(hitTag))
        {
            StopCoroutine(routine);
        }
    }

    public abstract IEnumerator doEffect(Collider other);

    public virtual void OnDisable() 
    {
        StopCoroutine(routine);
    }
}
