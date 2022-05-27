using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RockManager : MonoBehaviour
{

    private static RockManager instance;
    public static RockManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("No RockManager in the scene");
            }
            return instance;
        }
    }

    public List <GameObject> allRocks = new List <GameObject>();

    private int RockMoved;
    private int initialRockCount;
    private float rockX;
    private float rockZ;
    public GameObject rockPrefab;
    public bool countUnderWantedRocks;
    private BossManager bManager;


    public int amountToSpawnNew;

    void Awake()
    {
        bManager = GetComponent<BossManager>();
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
        allRocks = GameObject.FindGameObjectsWithTag("rocks").ToList();
        initialRockCount = allRocks.Count;
        // for(int i=0; i<allRocks.Count; i++){ //testing that the objects were adding
        //             Debug.Log(allRocks[i]);
        // }
       // Debug.Log(allRocks[0].transform.position); //how to find position for rock
    }


  //  void Update()
  //  {
     //   if(amountToSpawnNew<=allRocks.Count)
     //   {
            //call boss manager which will call SpawnNewRocks()
            //maybe something that changes a bool or something which then gets checked by the boss manager
    //    }
    //    if (Input.GetButtonDown("Teleport"))
    //    {
       //     SpawnNewRocks();
     //   }
 //   }

    // void Update()
    // {
    //     if(allRocks.Count <= bManager.spawnRocksNumber)
    //     {
    //         countUnderWantedRocks = true;
    //     }
    // }



    //update the array with the new position
    public void RockPositionUpdate(GameObject rockMoved)
    {
        allRocks[RockMoved]=rockMoved;
        Debug.Log(allRocks[RockMoved].transform.position);
    }

    //finds closes rock to the boss
    public GameObject FindClosesRock(Transform bossPos) //needs to give pos of boss
    {
        allRocks.RemoveAll(s => s == null);


        int closesRock=0;

        float lowestValue=10000;


        for(int i=0; i<allRocks.Count; i++){
            float tempx=allRocks[i].transform.position.x-bossPos.position.x;
            float tempz=allRocks[i].transform.position.z-bossPos.position.z;
            float distanceFromBoss=(tempx*tempx)+(tempz*tempz);
            if(distanceFromBoss<lowestValue)
            {
                lowestValue=distanceFromBoss;
                closesRock=i;
            }
        }
        RockMoved=closesRock;

        GameObject closest=allRocks[closesRock];
        //update closest health
        RockPickedUp closesttesting = closest.GetComponent<RockPickedUp>();
        closesttesting.MakeRockInv();
        return closest;
    }

    //used by rockmanager to see if there is any rocks left
    public bool IsThereStillRocks()
    {
        if(allRocks.Count<=0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SpawnNewRocks()
    {
        GameObject newRock1= Instantiate(rockPrefab);
        newRock1.transform.position= new Vector3(Random.Range(-50.0f, -25.0f), 0, Random.Range(-50.0f, 0f));
        allRocks.Add(newRock1);
        GameObject newRock2= Instantiate(rockPrefab);
        newRock2.transform.position= new Vector3(Random.Range(-50.0f, -25.0f), 0, Random.Range(0f, 50f));
        allRocks.Add(newRock2);
        GameObject newRock3= Instantiate(rockPrefab);
        newRock3.transform.position= new Vector3(Random.Range(-25.0f, 0f), 0, Random.Range(-50f, 0f));
        allRocks.Add(newRock3);
        GameObject newRock4= Instantiate(rockPrefab);
        newRock4.transform.position= new Vector3(Random.Range(-25.0f, 0f), 0, Random.Range(0f, 50f));
        allRocks.Add(newRock4);
        GameObject newRock5= Instantiate(rockPrefab);
        newRock5.transform.position= new Vector3(Random.Range(0f, 25f), 0, Random.Range(-50f, 0f));
        allRocks.Add(newRock5);
        GameObject newRock6= Instantiate(rockPrefab);
        newRock6.transform.position= new Vector3(Random.Range(0f, 25f), 0, Random.Range(0f, 50f));
        allRocks.Add(newRock6);
        GameObject newRock7= Instantiate(rockPrefab);
        newRock7.transform.position= new Vector3(Random.Range(25f, 50f), 0, Random.Range(-50f, 0f));
        allRocks.Add(newRock7);
        GameObject newRock8= Instantiate(rockPrefab);
        newRock8.transform.position= new Vector3(Random.Range(25f, 50f), 0, Random.Range(0f, 50f));
        allRocks.Add(newRock8);
    }

    //needed for boss manager
    public void ClearUpList()
    {
        allRocks.RemoveAll(s => s == null);
    }


    // public void SpawnNewRocks()
    // {
    //     for(int i = 0; i < initialRockCount; i++)
    //     {
    //         GameObject respawnedRock = Instantiate(rockPrefab);
    //         newRock.transfrom.position = newVector3(Random.Range)
    //     }
    // }
    private void randomRockPos()
    {
        //whatevertheArenalimits are should determine what to input into random here
        rockX = Random.Range(0, 10); //values are placeholder
        rockZ = Random.Range(0, 10);
    }
}
