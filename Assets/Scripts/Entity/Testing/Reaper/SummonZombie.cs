using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonZombie : MonoBehaviour
{
    public GameObject zombie;
    public List<GameObject> zombieArray;
    public int maxZombies;
    public int numberPerSpawn;
    public float spawnRadius;
    public float spawnBuffer;

    public GameObject Boss;

    // Start is called before the first frame update
    void Start()
    {
        zombieArray = new List<GameObject>();
        Boss = GameObject.FindGameObjectWithTag("Boss");
        transform.position = Boss.transform.position;
        transform.forward = Boss.transform.forward;
        transform.rotation = Boss.transform.rotation;
        StartCoroutine(Summon());
    }

    public IEnumerator Summon()
    {
        int toSpawn = (getAliveZombies() + numberPerSpawn) > maxZombies ? (maxZombies - getAliveZombies()) : numberPerSpawn;

        for(int i = 0; i < toSpawn; i++)
        {
            Vector3 randomFactor = (Random.insideUnitCircle * spawnRadius);
            Vector3 spawnPos = transform.position + randomFactor;
            zombieArray.Add(Instantiate(zombie, spawnPos, transform.rotation));
            yield return new WaitForSeconds(spawnBuffer);
        }
        Destroy(gameObject);
    }

    public int getAliveZombies() 
    {
        try
        {
            GameObject.FindGameObjectWithTag("hordeEnemy");
            return GameObject.FindGameObjectsWithTag("hordeEnemy").Length;
        }
        catch
        {
            return 0;
        }
    }
}
