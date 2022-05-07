using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour
{

    public ParticleSystem part;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    public GameObject Player;
    public int damage = 5;

    public int hitCount = 0;

    //public List<ParticleCollisionEvent> collisionEvents;
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
       // collisionEvents = new List<ParticleCollisionEvent>();
    }

    /*void OnParticleCollision(GameObject other)
    {
        Debug.Log("swave");
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;
        while (i < numCollisionEvents)
        {
            if (rb)
            Debug.Log("Shockwave Hit!");
            /*{
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce(force);
            }
            i++;
        }
    }*/

    /*void update(){
        if(Input.GetButtonDown("SwingWep")){
            Debug.Log("Do Shockwave");
            part.Play();
        }
    }*/

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = part.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        Debug.Log("Shockwave Hits!!!");
        hitCount +=1;
        // iterate through the particles which entered the trigger and make them red
        /*for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(0, 255, 0, 255);
            enter[i] = p;
        }*/

        PlayerStats health = Player.GetComponent<PlayerStats>();

        if(health != null){
            //health.takeDamage(damage);
        }

        // iterate through the particles which exited the trigger and make them green
        /*for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(0, 255, 0, 255);
            exit[i] = p;
        }*/

        // re-assign the modified particles back into the particle system
        part.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }

    
}
