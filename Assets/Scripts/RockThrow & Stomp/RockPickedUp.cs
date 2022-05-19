using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPickedUp : MonoBehaviour
{
    private float speed=0f; //set to 10 for testing purposes
    public GameObject rockPrefab; //on moving rock
    private int rockHealth=3; //might move this into seperate script

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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


    //for bullet
    void OnCollisionEnter(Collision collider)
    {
        GameObject other = collider.gameObject;
         if(other.tag=="PlayerBullet")
         {
             rockHealth--;
         }
         if(rockHealth<=0)
         {
             Destroy(gameObject);
         }
    }
    //sword uses this instead of on collsion for some reason
    private void OnTriggerEnter(Collider other){
        if(other.tag=="sword")
         {
             rockHealth--;
         }
        if(rockHealth<=0)
         {
             Destroy(gameObject);
         }
    }
}
