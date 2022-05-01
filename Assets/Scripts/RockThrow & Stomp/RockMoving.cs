using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMoving : MonoBehaviour
{ 
    //private Transform target;
    public Transform target;
    //target will be converted to player
    private float translationZ;
    private float translationY;
    private float translationX;
    private float speed=20;
    private float rageMultiplier=1;

        private float trackZ;
    private float trackY;
    private float trackX;
    private Vector3 localArea;
    public GameObject rockPrefab;

    private GameObject player;
    private GameObject arena;
    // Start is called before the first frame update
    void Start()
    {
        arena=GameObject.FindWithTag("Arena");
        // player=GameObject.FindWithTag("Player"); or
       // player = GameObject.Find("Player"); //later boss script can send this value to this scripts
         player=GameObject.FindWithTag("Target");
         
        target=player.transform;
        //puts position into local space
        localArea= transform.InverseTransformPoint(target.position); 
        localArea.Normalize();
        //takes position and stores it for later use
        translationZ=localArea.z;
        translationY=localArea.y;
        translationX=localArea.x;
    }

    // Update is called once per frame
   void Update()
    {
        transform.Translate(translationX *(speed*rageMultiplier * Time.deltaTime), translationY *(speed*rageMultiplier* Time.deltaTime), translationZ *(speed*rageMultiplier * Time.deltaTime));
    }

    //spawn ground rock and destroy itself
    public void spawnRock()
    {
        GameObject breakablerock= Instantiate(rockPrefab); 
        breakablerock.transform.position=transform.position;
        Destroy(gameObject);
    }
    

    //this will be called to enable rage ability
    public void EnableRageMode()
    {
        rageMultiplier=2;
    }

    void OnCollisionEnter(Collision collider)
    {
        GameObject other = collider.gameObject;
        Debug.Log(other);

        if(other==player)
        {
            HealthTemp healthTemp = other.GetComponent<HealthTemp>();
            healthTemp.takeDamage(10);
            //damage player
            //takeDamage(some value) from health temp or where ever
        }
        if(other==arena)
        {
            spawnRock();
        }
    }
}
