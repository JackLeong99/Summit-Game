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
    private int rockNumberMin;
    public GameObject rockPrefab;
    public bool countUnderWantedRocks;
    //private BossManager bManager;
    private GameObject boss; //create a gameobject reference


    public int amountToSpawnNew;

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
        allRocks = GameObject.FindGameObjectsWithTag("rocks").ToList();
        initialRockCount = allRocks.Count;
        boss = GameObject.FindWithTag("Boss"); //find the tag of the object you want ie the boss's tag in this case
        //bManager = boss.GetComponent<BossManager>(); //same as before except added boss the gameObject before getComponent
        //rockNumberMin = bManager.rockNumberMinimum();
        // for(int i=0; i<allRocks.Count; i++){ //testing that the objects were adding
        //             Debug.Log(allRocks[i]);
        // }
       // Debug.Log(allRocks[0].transform.position); //how to find position for rock
    }

    void Update()
    {
        if(allRocks != null && allRocks.Count <= rockNumberMin)
        {
            countUnderWantedRocks = true;
        }
    }

    //update the array with the new position
    public void RockPositionUpdate(GameObject rockMoved)
    {
        //allRocks[RockMoved]=rockMoved;
       // Debug.Log(allRocks[RockMoved].transform.position);
        allRocks.Add(rockMoved);
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
        int rocksToSpawn = 8;
        int increase = 25;
        float rockRangeDown = -50f;
        float rockRangeUp = -25f;
        float constant = -50f;

        for (int i = 1; i < rocksToSpawn / 2; i++)
        {
            allRocks.Add(Instantiate(rockPrefab));
            allRocks.Last().transform.position = new Vector3(Random.Range(rockRangeDown, rockRangeUp), -2, Random.Range(constant, constant + 50f));
            allRocks.Last().GetComponent<RockPickedUp>().SpawnUp();

            if (rocksToSpawn % 2 != 0)
            {
                constant = 0;
            }
            else
            {
                rockRangeDown += increase;
                rockRangeUp += increase;
                constant = -50f;
            }
        }
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
