using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer2 : MonoBehaviour
{
    public string attackName;
    [SerializeField] float damage = 25f;
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            PlayerStats health = other.GetComponent<PlayerStats>();

            if(health != null){
                GameManager.Instance.onPlayerHit(attackName);
                health.takeDamage(damage);
            }
        }
        
    }//timeout / delay- anti-multi-hit to-add

    public void rageAttackModifier(){
        damage = damage * 1.25f;
    }
}
