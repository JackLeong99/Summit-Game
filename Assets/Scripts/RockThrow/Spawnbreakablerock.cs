using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnbreakablerock : MonoBehaviour
{

    public GameObject rockPrefab; //might want to set privately possiblely
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        spawnRock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawner function only since we don't use timer we don't need start or update
    //might need find if I am going to set rock prefab dyamically or not since this is going to be set on a prefab object



    //for testing purposes I can put it on player and use a button to spawn non-collider rock
    public void spawnRock(){ //this would need to take the current "health" of the rock before it was thrown
        target=transform;
        GameObject breakablerock= Instantiate(rockPrefab); 
         breakablerock.transform.position=target.position;
         //might want to set the y myself
    }
}
