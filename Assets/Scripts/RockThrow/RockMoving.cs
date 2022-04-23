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
    // Start is called before the first frame update
    void Start()
    {
        //GameObject test=GameObject.FindWithTag("Player"); or
       // GameObject player = GameObject.Find("Player"); //later boss script can send this value to this scripts
        //target=player.transform;
        GameObject player=GameObject.FindWithTag("Target");
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
       
       
       //if(transform.position.y<=ground level) set a ground level for it to spawn at
        if(transform.position.y<=target.transform.position.y)
        {
            Debug.Log("hi");
            spawnRock();
            
        }
    }

    //spawn ground rock and destroy itself
    public void spawnRock()
    {
        GameObject breakablerock= Instantiate(rockPrefab); 
        breakablerock.transform.position=target.position;
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

        if(other==player)
        {
            HealthTemp healthTemp = other.GetComponent<HealthTemp>();
            healthTemp.takeDamage(10);
            //damage player
            //takeDamage(some value) from health temp or where ever
        }
    }
}
