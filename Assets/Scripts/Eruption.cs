using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eruption : MonoBehaviour
{
    [SerializeField] public GameObject warning;
    [SerializeField] public GameObject hitboxObject;
    [SerializeField] public GameObject target;
    [SerializeField] public float duration;
    [SerializeField] public float delay;
    [SerializeField] public int layer;

    //Update is for testing purposes without a boss script, should be removed in final use
    void Update()
    {
        if(Input.GetKeyDown("z"))
        {
            eruption();
        }
    }
    public void eruption()
    {
        StartCoroutine(erupt());
    }

    IEnumerator erupt()
    {
        int layerMask = 1 << layer;
        Debug.Log(layerMask);
        RaycastHit hit;
        if (Physics.Raycast(target.transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Vector3 targetPoint = hit.point;
            var prehit = Instantiate(warning, hit.point, Quaternion.identity);
            prehit.transform.localPosition += new Vector3(0, -0.3f, 0);
            yield return new WaitForSeconds(delay);
            var hitbox = Instantiate(hitboxObject, prehit.transform.position, Quaternion.identity);
            hitbox.transform.localPosition += new Vector3(0, -0.05f, 0);
            Destroy(prehit);
            yield return new WaitForSeconds(duration);
            Destroy(hitbox);
            Debug.DrawRay(target.transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow, 3);
            //Debug.Log("Did Hit");
        } 
        else
        {
            var prehit = Instantiate(warning, target.transform.position, Quaternion.identity);
            prehit.transform.localPosition += new Vector3(0, -0.3f, 0);
            yield return new WaitForSeconds(delay);
            var hitbox = Instantiate(hitboxObject, prehit.transform.position, Quaternion.identity);
            hitbox.transform.localPosition += new Vector3(0, -0.05f, 0);
            Destroy(prehit);
            yield return new WaitForSeconds(duration);
            Destroy(hitbox);
        }
    }
}
