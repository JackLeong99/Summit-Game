using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;
    public GameObject oasisOrbPrefab;
    public GameObject oasisHealingPrefab;
    public int minXPos=0;
    public int maxXPos=50;
    public int minZPos=0;
    public int maxZPos=50;

    private int currentOrbsSpawned;

    private float timeForBossToHeal=3f;

    private float defaultTimeForBossToHeal=3f;

    private bool disableShield=false;
    
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
    void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
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
        if(disableShield)
        {
            timeForBossToHeal-=Time.deltaTime;
            if(timeForBossToHeal<=0)
            {
                DestroyShield();
                timeForBossToHeal=defaultTimeForBossToHeal;
                disableShield=false;
            }
        }
    }


    //this is called by sandstorm or moved into sandstorm script
    public void OrbCreation()
    {
        for(int i=0; i<3; i++)
        {
            int xPos=Random.Range(minXPos, maxXPos);
            //temp fix to prevent orbs from going into oasis
            if(xPos<10 && xPos>-10) 
            {
                xPos=11;
            }
            int zPos=Random.Range(minZPos, maxZPos);
            if(zPos<10 && zPos>-10)
            {
                zPos=11;
            }
       // GameObject orb = 
            Instantiate(orbPrefab,new Vector3(xPos, 1, zPos),Quaternion.identity);
        }
        currentOrbsSpawned=3;
        Instantiate(oasisOrbPrefab,new Vector3(0, 3, 0),Quaternion.identity);
        Instantiate(oasisHealingPrefab,new Vector3(0, 1, 0),Quaternion.identity);
    }

    public void OrbDestroyed()
    {
        currentOrbsSpawned--;
        if(currentOrbsSpawned==0)
        {
            DestroyShield();
        }
    }

    public void DestroyShield()
    {
        Destroy(GameObject.FindGameObjectWithTag("shield")); //or whatever tag it has
    }

    public void Failure()
    {
        Destroy(GameObject.FindGameObjectWithTag("oasisOrb"));
        //mage heals. Not sure if we have heal for boss and how to reference it
        Destroy(GameObject.FindGameObjectWithTag("oasis"));
        disableShield=true;
    }
}
