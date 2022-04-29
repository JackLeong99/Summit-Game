using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleport : MonoBehaviour
{


    //need to prevent the player from going outside the arena
    //could get a function that finds this out

    private float newX;
    private float newY;
    private float newZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Teleport"))
    {
        TeleportRandom();
    }
    }

    public void TeleportRandom() 
    {

        newX=Random.Range(-10f, 10f);
        newY=Random.Range(3f, 6f);
        newZ=Random.Range(-10f, 10f);
        transform.Translate(newX, newY, newZ);
    }
}
