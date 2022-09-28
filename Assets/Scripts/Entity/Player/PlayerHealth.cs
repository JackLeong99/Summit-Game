using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;



public class PlayerHealth : MonoBehaviour
{
    private Dodge dodge;
    public float maxHealth;
    public float currentHealth;
    public bool invulnerable = false;

    //added at my own liberty- figure it'll be useful down the line.
    public float defence = 0f;

    //Used by DecreaseEnemyAttack power-up
    private bool lowerdamage=false;


    private void Awake()
    {
        dodge = GetComponent<Dodge>();
    }
    void Start()
    {
        //prevents bugs- always start with max hp- can't set to start higher than max.
        currentHealth = maxHealth;
    }

    public void takeDamage(float damage){
        if (invulnerable) 
        {
            return;
        }

        if (currentHealth > 0){
            if(lowerdamage)
            {
                damage-=5; //we can change this. I was not sure how much to decrease by
            }
            //Not healing if defence stat bigger than potential damage taken.
            if(damage - defence > 0){
                currentHealth = currentHealth - damage + defence;
            }
            
        }
        AkSoundEngine.PostEvent("Player_Damage", gameObject);
        CameraListener.instance.CameraShake(damage, 0.25f);

        if (currentHealth <= 0)
        {
            GameManager.instance.LoadDelegate(GameManager.instance.OnDeath()); //to be moved to whatever is handling health
        }
    }


//Doesn't work for some reason- revisit.
    public void healDamage(float healing){

        currentHealth += healing;
        //Prevents Overhealing
        if (currentHealth >= maxHealth){
            currentHealth = maxHealth;
        }
        //System.Console.WriteLine("current health = " + currentHealth + "after taking " + healing);
    }

    //public void DecreaseDamage()
    //{
    //    if(lowerdamage==true)
    //    {
    //        lowerdamage=false;
    //    }
    //    else
    //    {
    //       lowerdamage=true; 
    //    }
    //}

    //used by gamemanger
    public float GetPlayerHealth()
    {
        return currentHealth;

    }
}    
