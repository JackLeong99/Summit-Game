using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer2 : MonoBehaviour
{
    //made public so it can be different for different enemy types without needing many different scripts
    //of the same kind
    public int damage = 25;
    private void OnTriggerEnter(Collider other){
        PlayerStats health = other.GetComponent<PlayerStats>();

        if(health != null){
            health.takeDamage(damage);
        }
    }
}
