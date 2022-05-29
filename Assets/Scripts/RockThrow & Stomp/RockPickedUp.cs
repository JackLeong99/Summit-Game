using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPickedUp : MonoBehaviour
{
    public GameObject rockPrefab; //on moving rock
    private int rockHealth=3; //might move this into seperate script
    public GameObject rockHand;
    
    private float timer=1.2f; //modify this to get it release better
    private bool timerActive=false;
    public GameObject sparksPrefab;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rockHand = GameObject.FindWithTag("rockHold");
        player = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive)
        {
            timer-=Time.deltaTime;
        }
        if(timer<=0)
        {
            ReachedTop();
        }
    }

    //when it reaches the top it will be destroyed and a moving rock will be spawned
    public void ReachedTop()
    {
        //spawns rock for throwing
         GameObject thrownRock = Instantiate(rockPrefab,transform.position,Quaternion.identity); 
        Destroy(gameObject);

    }

    //code for the rock moving up for when it is "picked up"
    public void PickedUp()
    {
        this.transform.SetParent(rockHand.transform);
        transform.localPosition = new Vector3(0, 0, 0);
        timerActive=true;
    }


    //detects hits on rock
    private void OnTriggerEnter(Collider other){
        if(other.tag=="sword" || other.tag == "PlayerBullet")
         {
            rockHealth--;
            Instantiate(sparksPrefab, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
            //TODO add hit particles
         }
        if(rockHealth<=0)
         {
            Destroy(gameObject);
            player.GetComponent<PlayerStats>().healDamage(5.0f);
         }
    }

    public void MakeRockInv()
    {
        rockHealth=1000000000;
    }
}
