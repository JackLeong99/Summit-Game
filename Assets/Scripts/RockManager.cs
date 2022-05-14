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

    private List <GameObject> allRocks = new List <GameObject>();

    private int RockMoved;

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
        allRocks=GameObject.FindGameObjectsWithTag ("rocks").ToList();
        // for(int i=0; i<allRocks.Count; i++){ //testing that the objects were adding
        //             Debug.Log(allRocks[i]);
        // }
       // Debug.Log(allRocks[0].transform.position); //how to find position for rock
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        //only need x and z

        //GameObject closest;
        int closesRock=0;

        float lowestValue=10000;


        for(int i=0; i<allRocks.Count; i++){
            float tempx=allRocks[i].transform.position.x-bossPos.position.x;
            float tempz=allRocks[i].transform.position.z-bossPos.position.z;
            float distanceFromBoss=(tempx*tempx)+(tempz*tempz);
      //  Debug.Log(distanceFromBoss);
            if(distanceFromBoss<lowestValue)
            {
                lowestValue=distanceFromBoss;
                closesRock=i;
               // Debug.Log(i);
            }


            //x2 + z2 find smallest
        }
        RockMoved=closesRock;

        GameObject closest=allRocks[closesRock];
        return closest;
        //need to return something
    }
}
