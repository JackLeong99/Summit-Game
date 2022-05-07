using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    private bool SlamIsRunning=false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter(Collision collider)
    {
        GameObject other = collider.gameObject;
         if(other==player)
        {
            if (SlamIsRunning)
            {
            HealthTemp healthTemp = other.GetComponent<HealthTemp>();
            healthTemp.takeDamage(10);
            }
        }

    }

    public void EnableSlam()
    {
        SlamIsRunning=true;
    }

    public void DisableSlam()
    {
        SlamIsRunning=false;
    }


}
