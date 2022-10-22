using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [Header("Reference")]
    public Slider healthbar;

    public void Start()
    {
        healthbar.maxValue = GameManager.instance.player.GetComponent<PlayerHealth>().maxHealth;
    }

    public void Update()
    {
        switch (true)
        {
            case bool x when GameManager.instance.player != null:

                var health = GameManager.instance.player.GetComponent<PlayerHealth>();
                if (healthbar.maxValue != health.maxHealth) { healthbar.maxValue = health.maxHealth; }

                healthbar.value = health.currentHealth;
                break;
        }
    }
}