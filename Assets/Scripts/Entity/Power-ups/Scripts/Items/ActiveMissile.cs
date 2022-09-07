using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/activeMissile")]

public class ActiveMissile : ItemBase
{
    public GameObject projectile;

    public float missileCount;
    public float projectileSpeed;
    public float tracking;
    public float damage;
    public float bufferTime;
    public override void effect(GameObject target)
    {
        target.GetComponent<ActiveItem>().StartCoroutine(fire(target));
    }

    public IEnumerator fire(GameObject target) 
    {
        List<GameObject> projectileList = new List<GameObject>();

        for (int i = 0; i < missileCount; i++) 
        {
            projectileList.Add(Instantiate(projectile, target.transform.position + new Vector3(0, 5, 0), Quaternion.identity) as GameObject);
        }

        foreach (var p in projectileList) 
        {
            p.GetComponent<Missile>().setTracking(tracking);
            p.GetComponent<Rigidbody>().velocity = (Vector3.up + new Vector3(Random.Range(-2, -2), 0, Random.Range(-2, -2))) * projectileSpeed;
            p.GetComponent<Missile>().SetDamage(damage);
            yield return new WaitForSeconds(bufferTime);
        }
    }
}
