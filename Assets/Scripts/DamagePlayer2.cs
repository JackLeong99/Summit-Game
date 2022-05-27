using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer2 : MonoBehaviour
{
    public string attackName;
    [SerializeField] float damage;
    private BossManager bManager;

    private void Awake(){
        bManager = GetComponent<BossManager>();
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            PlayerStats health = other.GetComponent<PlayerStats>();

            if(health != null){
                GameManager.Instance.onPlayerHit(attackName);
                if(bManager.rage == true){
                    damage = damage * bManager.rageAttackMultiplier;
                }
                health.takeDamage(damage);
            }
        }
        
    }//timeout / delay- anti-multi-hit to-add
}
