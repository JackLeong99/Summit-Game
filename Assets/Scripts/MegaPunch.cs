using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaPunch : MonoBehaviour
{
    [SerializeField] public GameObject hitboxObject;
    [SerializeField] public GameObject parentObject;

    [SerializeField] public float duration;

    void Update()
    {
        if(Input.GetKeyDown("x"))
        {
            megaPunch();
        }
    }
    public void megaPunch()
    {
        StartCoroutine(punch());
    }

    IEnumerator punch()
    {
        var hitbox = Instantiate(hitboxObject, parentObject.transform.position, Quaternion.identity, parentObject.transform);
        yield return new WaitForSeconds(duration);
        Destroy(hitbox);
    }
}
