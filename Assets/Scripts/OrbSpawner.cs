using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;
    public int minXPos=0;
    public int maxXPos=50;
    public int minZPos=0;
    public int maxZPos=50;

    private int currentOrbsSpawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Teleport"))
        {
            OrbCreation();
        }
    }


    //this is called by sandstorm or moved into sandstorm script
    public void OrbCreation()
    {
        for(int i=0; i<3; i++)
        {
            int xPos=Random.Range(minXPos, maxXPos);
            int zPos=Random.Range(minZPos, maxZPos);
       // GameObject orb = 
            Instantiate(orbPrefab,new Vector3(xPos, 3, zPos),Quaternion.identity);
        }
        currentOrbsSpawned=3;
        //50 by 50
        //when setting up scene location of oasis will need to be inputted into this
    }

    public void OrbDestroyed()
    {
        currentOrbsSpawned--;
        if(currentOrbsSpawned==0)
        {
            //function to disable shield
        }
    }
    //need to create function to see how many orbs are left
    //need script for orb themself
}
