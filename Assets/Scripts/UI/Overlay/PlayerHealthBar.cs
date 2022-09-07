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
                healthbar.value = GameManager.instance.player.GetComponent<PlayerHealth>().currentHealth;
                break;
        }
    }
}