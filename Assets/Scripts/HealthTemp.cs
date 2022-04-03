using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealthTemp : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage){
        currentHealth = currentHealth - damage;
        System.Console.WriteLine("current health = " + currentHealth + "after taking " + damage);
    }
}    
