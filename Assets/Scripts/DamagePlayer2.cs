using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer2 : MonoBehaviour
{
    [SerializeField] int damage = 25;
    private void OnTriggerEnter(Collider other){
        PlayerStats health = other.GetComponent<PlayerStats>();

        if(health != null){
            health.takeDamage(damage);
        }
    }
}
