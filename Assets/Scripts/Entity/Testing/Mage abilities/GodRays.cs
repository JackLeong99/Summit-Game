using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodRays : MonoBehaviour
{
    public float rayCount;

    public float spreadRange;

    public float delay;

    public float duration;

    public float damage;

    public float tickRate;

    public GameObject indicator;

    public GameObject ray;

    public void Update()
    {
        if (Input.GetKeyDown("h")) 
        {
            StartCoroutine(doRays());
        }
    }
    public IEnumerator doRays()
    {
        GameObject target = GameManager.player;
        List<GameObject> rayList = new List<GameObject>();
        List<GameObject> damageRayList = new List<GameObject>();

        rayList.Add(Instantiate(indicator, new Vector3(target.transform.position.x, 25.0f, target.transform.position.z), Quaternion.identity) as GameObject);

        for (int i = 0; i  < rayCount-1; i++) 
        {
            rayList.Add(Instantiate(indicator, new Vector3(target.transform.position.x + Random.Range(-spreadRange, spreadRange), 25.0f, target.transform.position.z + Random.Range(-spreadRange, spreadRange)), Quaternion.identity) as GameObject);
        }

        yield return new WaitForSeconds(delay);

        foreach (var rays in rayList) 
        {
            var r = Instantiate(ray, rays.transform.position, Quaternion.identity) as GameObject;
            r.GetComponent<LightBeam>().setDamage(damage, tickRate);
            damageRayList.Add(r);
            Destroy(rays);
        }

        yield return new WaitForSeconds(duration);

        foreach (var rays in damageRayList) 
        {
            Destroy(rays);
        }
    }
}
