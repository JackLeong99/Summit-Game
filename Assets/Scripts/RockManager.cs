using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RockManager : MonoBehaviour
{

    private List <GameObject> allRocks = new List <GameObject>();
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

    public void RockPositionUpdate(int postionInArray)
    {
        //update the array with the new position
    }

    public GameObject FindClosesRock(Transform bossPos) //needs to give pos of boss
    {
        //only need x and z

        //GameObject closest;
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


            //x2 + z2 find smallest
        }

        GameObject closest=allRocks[closesRock];
        return closest;
        //need to return something
    }
}
