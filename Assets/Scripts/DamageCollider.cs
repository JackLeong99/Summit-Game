using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DamageCollider : MonoBehaviour
{
    [SerializeField] int currentWeaponDamage = 50;

//these two functions aren't really 'necessary' right now but no harm in leaving them in. Might be useful later.
    private void OnTriggerEnter(Collider other){
        //If the weapon is 'swinging' and an enemy is hit
        if(other.tag == "Hittable"){
            //Store the gameObject that is hit in a variable
            HealthTemp healthTemp = other.GetComponent<HealthTemp>();
            //Deal damage if a health script exists.
            if (healthTemp != null){
                healthTemp.takeDamage(currentWeaponDamage);
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(other);
        }
    }
}
