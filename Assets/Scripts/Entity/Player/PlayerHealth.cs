using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;



public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float defence;
    public float shakeScale;
    [HideInInspector]
    public bool invulnerable = false;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(float damage){
        if (invulnerable) 
        {
            return;
        }
        AkSoundEngine.PostEvent("Player_Damage", gameObject);
        CameraListener.instance.CameraShake(damage * shakeScale, 0.25f);
        damage -= defence;
        damage = Mathf.Clamp(damage, 0f, Mathf.Infinity);
        switch (true) 
        {
            case bool x when currentHealth - damage > 0:
                currentHealth -= damage;
                break;
            default:
                GameManager.instance.LoadDelegate(GameManager.instance.OnDeath());
                break;
        }
    }

    public void healDamage(float healing)
    {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    public float GetPlayerHealth()
    {
        return currentHealth;
    }
}    
