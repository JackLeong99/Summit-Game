using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    private GameObject spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = GameObject.FindGameObjectWithTag("enemyHitbox");
        transform.position = spawnPos.transform.position;
        //Smoke();
    }
}
