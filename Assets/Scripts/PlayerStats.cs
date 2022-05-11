using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;



public class PlayerStats : MonoBehaviour
{
    private Dodge dodge;
    public int maxHealth;
    public int currentHealth;

    //added at my own liberty- figure it'll be useful down the line.
    public int defence = 0;
    private void Awake()
    {
        dodge = GetComponent<Dodge>();
    }
    void Start()
    {
        //prevents bugs- always start with max hp- can't set to start higher than max.
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage){
        if (currentHealth > 0){
            //Not healing if defence stat bigger than potential damage taken.
            if(damage - defence > 0 && !dodge.isDodging){
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
