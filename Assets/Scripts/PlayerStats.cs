using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    //added at my own liberty- figure it'll be useful down the line.
    public int defence = 0;
    // Start is called before the first frame update
    void Start()
    {
        //prevents bugs- always start with max hp- can't set to start higher than max.
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage){
        if (currentHealth > 0){
            //Not healing if defence stat bigger than potential damage taken.
            if(damage - defence > 0){
                currentHealth = currentHealth - damage + defence;
            }
            
        }
        UIManager.Instance.HealthBarSet(currentHealth);
        System.Console.WriteLine("current health = " + currentHealth + "after taking " + damage);
    }


//Doesn't work for some reason- revisit.
    public void healDamage(int healing){

        currentHealth = currentHealth + healing;
        //Prevents Overhealing
        if (currentHealth >= maxHealth){
            currentHealth = maxHealth;
        }
        UIManager.Instance.HealthBarSet(currentHealth);
        System.Console.WriteLine("current health = " + currentHealth + "after taking " + healing);
    }
}    
