using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPowerup : MonoBehaviour
{
    public ItemBase itemBase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            itemBase.effect(other.gameObject);
            Destroy(gameObject);
        }
    }
}
