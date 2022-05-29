using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMoving : MonoBehaviour
{ 
    public Transform target;
    private float translationZ;
    private float translationY;
    private float translationX;
    private float speed=120; //might need to edit based on distance from player //could check this before throw and set speed based on that
    private float rageMultiplier=1;

        private float trackZ;
    private float trackY;
    private float trackX;
    private Vector3 localArea;
    public GameObject rockPrefab;
    public ParticleSystem rockParticle;

    private GameObject player;

    private bool hasntHit=true;

    // Start is called before the first frame update
    void Start()
    {
         player=GameObject.FindWithTag("Player");
         
        target=player.transform;
        target.position=new Vector3(player.transform.position.x, player.transform.position.y+2 , player.transform.position.z);
        //this sets the target a bit above the player so it hits more often

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
        //Instantiate at the position all in one line
        GameObject breakablerock= Instantiate(rockPrefab,new Vector3(transform.position.x, transform.position.y-1 , transform.position.z), Quaternion.identity);
        RockManager.Instance.RockPositionUpdate(breakablerock);

        //creates the particle effect for landing
        ParticleSystem rockParticles= Instantiate(rockParticle,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    

    //this will be called to enable rage ability
    public void EnableRageMode()
    {
        rageMultiplier=2;
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        Debug.Log(other);

        if(other==player && hasntHit)
        {
            PlayerStats health = other.GetComponent<PlayerStats>();
            health.takeDamage(25);
            hasntHit=false;
            GameManager.Instance.onPlayerHit("rock throw");
        }
        
        if(other.tag=="Arena")
        {
            spawnRock();
        }

        if(other.tag=="worldBorder")
        {
            translationX=0;
            translationZ=0;
        }
    }
}