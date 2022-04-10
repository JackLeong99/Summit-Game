using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;
    public int currentWeaponDamage = 50;

    private bool toggled;

//testing code

    void Start(){
        //temp
        EnableDamageCollider();
        //DisableDamageCollider();
    }

//to help deetermine if awake.
    private void Awake(){
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableDamageCollider(){
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider(){
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Hittable" || damageCollider.enabled == true){
            HealthTemp healthTemp = other.GetComponent<HealthTemp>();

            if (healthTemp != null){
                healthTemp.takeDamage(currentWeaponDamage);
            }
        }
    }
//do last- Not yet working.
/*
    private void ToggleDamageCollider(){
        toggled = damageCollider.enabled;
        if(toggled == true){
            DisableDamageCollider();
            return;
        }
        
        EnableDamageCollider();
        
    
    }

    void update(){
        if(Input.GetMouseButtonDown(0)){
            ToggleDamageCollider();
        }
    }

    /*private void ToggleDamageCollider(object sender, System.Windows.Forms.KeyEventArgs e){
        if(e.KeyCode == Keys.Enter){
            if (damageCollider.enabled == false){
                EnableDamageCollider();
                wait 1;
            }
            DisableDamageCollider();
        }
    }*/
}
