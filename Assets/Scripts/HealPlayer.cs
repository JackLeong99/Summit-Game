using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    private int healing = 25;
    private void OnTriggerEnter(Collider other){
        PlayerStats health = other.GetComponent<PlayerStats>();

        if(health != null){
            health.healDamage(healing);
        }
    }
}
