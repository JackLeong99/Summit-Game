using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageRock : MonoBehaviour
{
    public GameObject rockPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Teleport"))
        {
            SpawnRocks();
        }
    }

    //currently not random as it just needs functionality for alpha
    public void SpawnRocks()
    {
        GameObject thrownRock = Instantiate(rockPrefab,new Vector3(transform.position.x-5, -1, transform.position.z+5),Quaternion.identity);
        thrownRock.GetComponent<MageRockMove>().LaunchTime(3.0f);
        //will need to set timer for all of these so they go one at a time
        GameObject thrownRock2 = Instantiate(rockPrefab,new Vector3(transform.position.x+5, -1, transform.position.z+5),Quaternion.identity);
        thrownRock2.GetComponent<MageRockMove>().LaunchTime(5.0f);
        GameObject thrownRock3 = Instantiate(rockPrefab,new Vector3(transform.position.x-5, -1, transform.position.z-5),Quaternion.identity);
        thrownRock3.GetComponent<MageRockMove>().LaunchTime(7.0f);
        GameObject thrownRock4 = Instantiate(rockPrefab,new Vector3(transform.position.x+5, -1, transform.position.z-5),Quaternion.identity);
        thrownRock4.GetComponent<MageRockMove>().LaunchTime(9.0f);
    }
}
