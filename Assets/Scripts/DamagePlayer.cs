using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private int damage = 25;
    private void OnTriggerEnter(Collider other){
        HealthTemp healthTemp = other.GetComponent<HealthTemp>();

        if(healthTemp != null){
            healthTemp.takeDamage(damage);
        }
    }
}
