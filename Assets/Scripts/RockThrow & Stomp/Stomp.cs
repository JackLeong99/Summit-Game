using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    
    private GameObject player;

    private float speed=5;
    private int stompTimes=0;
    private int rageStomp=0;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Target");
        //player=GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,speed* Time.deltaTime ,0);

        if(transform.position.y>=4)
        {
            OnTop();
            stompTimes--;
        }
        
        if(transform.position.y<=0)
        {
            speed=0;
        }
        if(transform.position.y<=0 && stompTimes>0)
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

    public void DoStomp()
    {
        stompTimes=1+rageStomp;
        speed=5;
    }

    void OnTop()
    {
        speed=-20;
    }

    public void EnableRageMode()
    {
        rageStomp=1;
    }
        
}
