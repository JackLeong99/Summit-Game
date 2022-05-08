using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaPunch : MonoBehaviour
{
    [SerializeField] public GameObject hitboxObject;
    [SerializeField] public GameObject parentObject;

    [SerializeField] public float duration;

    //Update is for testing purposes without a boss script, should be removed in final use
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
        var hitbox = Instantiate(hitboxObject, parentObject.transform.position, parentObject.transform.rotation, parentObject.transform);
        yield return new WaitForSeconds(duration);
        Destroy(hitbox);
    }
}
