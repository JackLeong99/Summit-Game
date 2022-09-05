using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaPunch : MonoBehaviour
{
    [SerializeField] public GameObject hitboxObject;
    [SerializeField] public GameObject parentObject;
    [SerializeField] public GameObject hitboxObjectB;
    [SerializeField] public GameObject parentObjectB;

    [SerializeField] public float duration;

    public void megaPunch()
    {
        StartCoroutine(punch());
    }

    IEnumerator punch()
    {
        var hitbox = Instantiate(hitboxObject, parentObject.transform.position, parentObject.transform.rotation, parentObject.transform);
        var hitboxB = Instantiate(hitboxObjectB, parentObjectB.transform.position, parentObjectB.transform.rotation, parentObjectB.transform);
        yield return new WaitForSeconds(duration);
        Destroy(hitbox);
        Destroy(hitboxB);
    }
}
