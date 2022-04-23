using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPickedUp : MonoBehaviour
{
    private float speed=0f; //set to 10 for testing purposes
    public GameObject rockPrefab; //on moving rock
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //call pickup when it is going to be thrown in air 
        //ie it can be damaged before it is thrown
        transform.Translate(0,speed* Time.deltaTime ,0);

        if(transform.position.y>=7) //y can be changed to any height
        {
            ReachedTop();
        }
    }

    //when it reaches the top it will be destroyed and a moving rock will be spawned
    public void ReachedTop()
    {
        //spawns rock for throwing
        GameObject thrownRock= Instantiate(rockPrefab); 
        thrownRock.transform.position=transform.position;
        Destroy(gameObject);

    }

    //code for the rock moving up for when it is "picked up"
    public void PickedUp()
    {
        speed=10f;
    }
}
