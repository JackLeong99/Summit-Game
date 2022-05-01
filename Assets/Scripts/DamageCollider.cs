using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;
    public int currentWeaponDamage = 50;

    private bool toggled;


//to help deetermine if awake.
    private void Awake(){
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }
//these two functions aren't really 'necessary' right now but no harm in leaving them in. Might be useful later.
    public void EnableDamageCollider(){
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider(){
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other){
        //If the weapon is 'swinging' and an enemy is hit
        if(other.tag == "Hittable" || damageCollider.enabled == true){
            //Store the gameObject that is hit in a variable
            HealthTemp healthTemp = other.GetComponent<HealthTemp>();
            //Deal damage if a health script exists.
            if (healthTemp != null){
                healthTemp.takeDamage(currentWeaponDamage);
            }
        }
    }

    void Update(){
        if(Input.GetButtonDown("SwingWep")){
            Debug.Log("SwingWep!");

            ToggleDamageCollider();
        }
    }

    private void ToggleDamageCollider(){
        //Temp Version Until animation exists
        toggled = damageCollider.enabled;
        if(toggled == false){
            EnableDamageCollider();
        }
        else{
            DisableDamageCollider();
        }

        
        //Actual version
        
        //Do Animation
        //EnableDamageCollider();
        //if(AnimationCompleted){
            //DisableDamage
        //}
    }
}
