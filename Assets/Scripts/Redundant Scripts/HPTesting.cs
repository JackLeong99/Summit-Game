using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPTesting : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    //added at my own liberty- figure it'll be useful down the line.
    public float defence = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //prevents bugs- always start with max hp- can't set to start higher than max.
        currentHealth = maxHealth;
    }

    public void takeDamage(float damage){
        if (currentHealth > 0f){
            //Not healing if defence stat bigger than potential damage taken.
            if(damage - defence > 0f){
                currentHealth = currentHealth - damage + defence;
            }   
        }
    }
}
