using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleport : MonoBehaviour
{


    //set a radius for where the player can go
    //need to prevent the player from going outside the arena
    //scale between -10 and 10 for x and z
    //get position of player
    //increase y by a scale of 3-6


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TeleportRandom() 
    {
        transform.position.y+=Random.Range(3f, 6f);
        transform.position.x+=Random.Range(-10f, 10f);
        transform.position.z+=Random.Range(-10f, 10f);
    }
}
