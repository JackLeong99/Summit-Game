using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley : MonoBehaviour
{
    public float damagePerTick;
    public float timeBetweenTicks;
    public float tickCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            tickCounter += Time.fixedDeltaTime;
            if(tickCounter >= timeBetweenTicks)
            {
                tickCounter = 0;
                PlayerHealth health = other.GetComponent<PlayerHealth>();

                if(health != null){
                    //GameManager.Instance.onPlayerHit(attackName);
                    health.takeDamage(damagePerTick);
                }
            }
        }
    }
}
