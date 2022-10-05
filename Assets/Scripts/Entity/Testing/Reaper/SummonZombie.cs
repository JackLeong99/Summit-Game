using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonZombie : MonoBehaviour
{
    public GameObject zombie;
    public GameObject[] zombieArray;
    public int maxZombies;
    public int numToSpawn;
    public int currentZombies;
    private Vector3 defaultSpawnPos;
    private Vector3 currentSpawnPos;
    private Vector3 spawnDirection;
    private Quaternion spawnRotation;
    public float spawnDistance;
    public int spawnsPerRow;
    private int numInCurrentRow;
    private int currentRow;

    public GameObject scuffedPositionSetBoss;

    // Start is called before the first frame update
    void Start()
    {
        zombieArray = new GameObject[maxZombies];
        scuffedPositionSetBoss = GameObject.FindGameObjectWithTag("enemyHitbox");
        transform.position = scuffedPositionSetBoss.transform.position;
        transform.forward = scuffedPositionSetBoss.transform.forward;
        transform.rotation = scuffedPositionSetBoss.transform.rotation;
        Summon();
    }

    public void Summon()
    {
        int count = 0;
        for(int i = 0; i < zombieArray.Length; i++)
        {
            if(zombieArray[i] != null)
            {
                count += 1;
            }
        }
        currentZombies = count;

        defaultSpawnPos = transform.position;
        spawnDirection = transform.forward;
        spawnRotation = transform.rotation;
        
        Vector3 spawnPos = defaultSpawnPos + spawnDirection * spawnDistance;

        for(int i = 0; i < maxZombies; i++)
        {
            if(zombieArray[i] == null && currentZombies < maxZombies)
            {
                if(numInCurrentRow < spawnsPerRow)
                {
                    zombieArray[i] = Instantiate(zombie, spawnPos, spawnRotation);
                    spawnPos = spawnPos * spawnDistance;
                }
                else
                {
                    currentRow += 1;
                    numInCurrentRow = 0;
                    spawnPos = defaultSpawnPos + spawnDirection * (spawnDistance * currentRow);
                    zombieArray[i] = Instantiate(zombie, spawnPos, spawnRotation);
                }
                currentZombies += 1;   
            }
        }
        Destroy(gameObject);
    }
}
