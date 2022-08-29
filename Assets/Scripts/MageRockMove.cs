using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageRockMove : MonoBehaviour
{
    private GameObject player;
    private float timer=15f;
    private bool needToGoUp=true;
    public float speed=120;

    private float topHeight=8f;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(needToGoUp)
        {
            transform.Translate(0,1*Time.deltaTime,0);
        }
        if(transform.position.y>=topHeight)
        {
            needToGoUp=false;
        }
        if(timer>0)
        {
            timer-=Time.deltaTime;
        }
        if(timer<=0)
        {
            target = new Vector3(player.transform.position.x, player.transform.position.y+2 , player.transform.position.z);
            GetComponent<Rigidbody>().velocity = (target - transform.position).normalized * speed;
        }
    }


    //function that sets timer for when it is to launch
    public void LaunchTime(float howLong)
    {
        timer=howLong;
    }
    
    void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        Debug.Log(other);

        if(other.tag=="player")
        {
            PlayerStats health = other.GetComponent<PlayerStats>();
            health.takeDamage(10);
        }
        
        if(other.tag=="Arena")
        {
            AkSoundEngine.PostEvent("Enemy_Boulder_Impact", gameObject);
            Destroy(gameObject);
        }
    }
}
