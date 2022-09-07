using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandPillar : MonoBehaviour
{
    public float maxHealth;

    public float health;

    public bool playerPresent;

    public bool active;

    public GameObject parent;

    public delegate void present(bool b);
    public static event present OnTrigger;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0) 
        {
            gameObject.transform.root.gameObject.SetActive(false);
        }
        if (playerPresent && active)
        {
            health -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (OnTrigger != null) 
            {
                OnTrigger(true);
                playerPresent = true;
                Debug.Log("player covered");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnTrigger != null)
            {
                OnTrigger(false);
                playerPresent = false;
            }
        }
    }

    public void toggleActive(bool b) 
    {
        active = b;            
    }

    public void OnEnable()
    {
        Sandstorm.OnStorm += toggleActive;
    }

    public void OnDisable()
    {
        if (OnTrigger != null)
        {
            OnTrigger(false);
            playerPresent = false;
        }
        Sandstorm.OnStorm -= toggleActive;
    }
}
