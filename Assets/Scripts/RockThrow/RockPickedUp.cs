using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPickedUp : MonoBehaviour
{
    private float speed=10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //call pickup when it is going to be thrown in air 
        //ie it can be damaged before it is thrown
    }

    //when it reaches the top it will be destroyed and a moving rock will be spawned
    public void ReachedTop()
    {
        //spawns rock for throwing
        Destroy(gameObject);

    }

    //code for the rock moving up for when it is picked up
    public void PickedUp()
    {
        transform.Translate(0,speed* Time.deltaTime ,0);
    }
}
