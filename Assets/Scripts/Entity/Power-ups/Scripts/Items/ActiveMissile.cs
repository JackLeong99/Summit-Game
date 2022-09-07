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
    public float delay;
    public override void effect(GameObject target)
    {
        target.GetComponent<ActiveItem>().StartCoroutine(fire(target));
    }

    public IEnumerator fire(GameObject target) 
    {
        List<GameObject> projectileList = new List<GameObject>();

        for (int i = 0; i < missileCount; i++) 
        {
            projectileList.Add(Instantiate(projectile, target.transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject);
        }

        float missileNo = delay;

        foreach (var p in projectileList) 
        {
            p.GetComponent<Missile>().setTracking(tracking, missileNo, FindClosestEnemy());
            p.GetComponent<Rigidbody>().velocity = (Vector3.up + new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f))).normalized * projectileSpeed;
            p.GetComponent<Missile>().SetDamage(damage);
            yield return new WaitForSeconds(bufferTime);
            missileNo += bufferTime;
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("enemyHitbox");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = GameManager.instance.player.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
