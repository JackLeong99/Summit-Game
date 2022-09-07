using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OasisHeal : MonoBehaviour
{
    private int healing = 25;
    private void OnTriggerEnter(Collider other){
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if(health != null){
            health.healDamage(healing);
            Destroy(gameObject);
        }
    }
}
