using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/activeMissile")]

public class ActiveMissile : ActiveAbility
{
    public GameObject projectile;
    public string seekTag;
    public float missileCount;
    public float projectileSpeed;
    public float projectileVelocity;
    public float tracking;
    public float damage;
    public float bufferTime;
    public float delay;
    public override void effect()
    {
        GameManager.instance.player.GetComponent<PlayerAbilities>().StartCoroutine(doEffect());
    }

    public override IEnumerator doEffect() 
    {
        List<GameObject> projectileList = new List<GameObject>();

        for (int i = 0; i < missileCount; i++) 
        {
            projectileList.Add(Instantiate(projectile, GameManager.instance.player.transform.position + new Vector3(0, 3, 0), Quaternion.Euler(-90,0,0)) as GameObject);
        }

        float missileNo = delay;
        float bonusDMG = Inventory.instance.abilityDamage;
        float dPercentMod = Inventory.instance.percentDamageMod;

        foreach (var p in projectileList) 
        {
            p.GetComponent<MeshRenderer>().enabled = true;
            p.GetComponent<Rigidbody>().velocity = (Vector3.up + new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f))).normalized * projectileSpeed;
            p.GetComponent<Missile>().setTracking(tracking, missileNo, projectileVelocity, FindClosestEnemy());
            p.GetComponent<Missile>().SetDamage((damage + bonusDMG) * dPercentMod);
            yield return new WaitForSeconds(bufferTime);
            missileNo += bufferTime;
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(seekTag);
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
