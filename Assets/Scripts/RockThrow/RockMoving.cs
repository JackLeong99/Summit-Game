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

    private Vector3 target2;

    private bool hasntHit=true;

    private float timer=20f;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Player");
        target=player.transform;
        //this sets the target a bit above the player so it hits more often
        target2 = new Vector3(player.transform.position.x, player.transform.position.y+2 , player.transform.position.z);
        //Adds velocity towards the player on spawn
        GetComponent<Rigidbody>().velocity = (target2 - transform.position).normalized * speed;
        RockManager.Instance.ClearUpList();
    }

    // Update is called once per frame
   void Update()
    {
        //transform.Translate(translationX *(speed*rageMultiplier * Time.deltaTime), translationY *(speed*rageMultiplier* Time.deltaTime), translationZ *(speed*rageMultiplier * Time.deltaTime));
        timer-=Time.deltaTime;
        if(timer<=0)
        {
            Destroy(gameObject);
        }
    }

    //spawn ground rock and destroy itself
    public void spawnRock()
    {
        //Instantiate at the position all in one line
        GameObject breakablerock = Instantiate(rockPrefab,new Vector3(transform.position.x, -0.5f, transform.position.z), Quaternion.identity);
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
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            health.takeDamage(30);
            hasntHit=false;
        }
        
        if(other.tag=="Arena")
        {
            AkSoundEngine.PostEvent("Enemy_Boulder_Impact", gameObject);
            spawnRock();
        }

        if(other.tag=="worldBorder")
        {
            translationX=0;
            translationZ=0;
        }
    }
}