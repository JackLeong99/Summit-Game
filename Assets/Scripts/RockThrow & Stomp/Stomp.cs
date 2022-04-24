using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    
    private GameObject player;

    private float speed=5;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,speed* Time.deltaTime ,0);

        if(transform.position.y>=4)
        {
            speed=-20;
        }
        
        if(transform.position.y<=0)
        {
            speed=5;
        }
    }

    //can be converte into an animation later rather than transform.translate

    void OnCollisionEnter(Collision collider)
    {
        GameObject other = collider.gameObject;
         if(other==player)
        {
            HealthTemp healthTemp = other.GetComponent<HealthTemp>();
            healthTemp.takeDamage(10);
        }

    }
        
}
