using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;
    public GameObject oasisOrbPrefab;
    public int minXPos=0;
    public int maxXPos=50;
    public int minZPos=0;
    public int maxZPos=50;

    private int currentOrbsSpawned;
    
    private static OrbSpawner instance;
    public static OrbSpawner Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("No OrbSpawner in the scene");
            }
            return instance;
        }
    }
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
        Instantiate(oasisOrbPrefab,new Vector3(0, 3, 0),Quaternion.identity);
    }

    public void OrbDestroyed()
    {
        currentOrbsSpawned--;
        if(currentOrbsSpawned==0)
        {
            DestroyShield();
        }
    }
    //need to create function to see how many orbs are left
    //need script for orb themself
    public void DestroyShield()
    {
        Destroy(GameObject.FindGameObjectWithTag("shield")); //or whatever tag it has
    }

    public void Failure()
    {
        //mage takes healing. Boss then disables everything
    }
}
