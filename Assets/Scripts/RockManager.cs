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
}
