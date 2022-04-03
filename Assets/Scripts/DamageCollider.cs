using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{/*
    Collider damageCollider;
    public int currentWeaponDamage = 50;

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

    private void OnTriggerEnter(Collider collision){
        if(collision.tag == "Hittable"){
            HealthTemp healthTemp = collision.GetComponent<HealthTemp>();

            if (healthTemp != null){
                healthTemp.takeDamage(currentWeaponDamage);
            }
        }
    }

    private void ToggleDamageCollider(object sender, System.Windows.Forms.KeyEventArgs e){
        if(e.KeyCode == Keys.Enter){
            if (damageCollider.enabled == false){
                EnableDamageCollider();
                wait 1;
            }
            DisableDamageCollider();
        }
    }*/
}
