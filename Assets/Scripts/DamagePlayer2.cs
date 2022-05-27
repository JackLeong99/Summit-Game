using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer2 : MonoBehaviour
{
    public string attackName;
    [SerializeField] float damage;
    // private GameObject[] bossTag;
    // private BossManager bManager;

    // private void Awake(){
    //     bossTag = GameObject.FindGameObjectsWithTag("Boss");
    //     if(bossTag != null){
    //         bManager = new bossTag[0].GetComponent<BossManager>();
    //     } 
    // }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            PlayerStats health = other.GetComponent<PlayerStats>();

            if(health != null){
                GameManager.Instance.onPlayerHit(attackName);
                // if(bManager.rage == true){
                //     damage = damage * bManager.rageAttackMultiplier;
                // }
                health.takeDamage(damage);
            }
        }
        
    }//timeout / delay- anti-multi-hit to-add
}
