using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer2 : MonoBehaviour
{
    public string attackName;
    [SerializeField] int damage = 25;
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            PlayerStats health = other.GetComponent<PlayerStats>();

            if(health != null){
                GameManager.Instance.onPlayerHit(attackName);
                health.takeDamage(damage);
            }
        }
        
    }//timeout / delay- anti-multi-hit to-add
}
