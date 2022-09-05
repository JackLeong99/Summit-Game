using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandPillar : MonoBehaviour
{
    public float maxHealth;

    private float health;

    public bool playerPresent;

    private void Start()
    {
        health = maxHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void OnTriggerExit(Collider other)
    {
        
    }
}
