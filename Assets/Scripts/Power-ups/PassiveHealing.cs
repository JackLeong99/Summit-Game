using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveHealing : MonoBehaviour
{
    private PlayerStats health;
    private float timer=5;
    private int healing=1;
    // Start is called before the first frame update
    void Start()
    {
        health=this.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0)
        {
        timer-=Time.deltaTime;
        }
        if(timer<0)
        {
            health.healDamage(healing);
            timer=5;
        }
    }
}
