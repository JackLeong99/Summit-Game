using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;



public class PlayerHealth : MonoBehaviour
{
    public ThirdPersonController controller;
    public float maxHealth;
    public float currentHealth;
    public float defence;
    private float defenceAsPercentage;
    public float shakeScale;
    [HideInInspector]
    public bool invulnerable = false;
    public float deathDuration = 4;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        defenceAsPercentage = (100 / (100 + defence));
    }

    public void takeDamage(float damage)
    {
        Debug.Log(invulnerable);
        if (invulnerable) { Debug.Log("Damage Taken"); return; }

        AkSoundEngine.PostEvent("Player_Damage", gameObject);
        CameraListener.instance.CameraShake(damage * shakeScale, 0.25f);
        UIDamageIn.instance.DamageVis();

        damage *= (100/(100 + defence));
        damage = Mathf.Clamp(damage, 0f, Mathf.Infinity);
        currentHealth -= damage;

        switch (Alive()) 
        {
            case false:
                StartCoroutine(Death());
                break;
            case true:
                EventManager.instance.OnHealthChange?.Invoke((currentHealth/maxHealth) * 100);
                EventManager.instance.OnTakeDamage?.Invoke(damage);
                break;
        }
    }

    public bool Alive()
    {
        return currentHealth > 0;
    }

    public void healDamage(float healing)
    {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        EventManager.instance.OnHealthChange?.Invoke((currentHealth / maxHealth) * 100);
    }
    
    public IEnumerator Death()
    {
        invulnerable = true;

        controller.stunned = ThirdPersonController.stunState.Stunned;
        controller._animator.SetTrigger("Death");

        yield return new WaitForSeconds(controller._animator.GetCurrentAnimatorStateInfo(0).length * 0.5f);
        yield return AnnouncementHandler.instance.Announcement("You Died.", deathDuration);

        HealthbarManager.instance.ClearBoss();
        GameManager.instance.LoadDelegate(GameManager.instance.OnDeath()); //to be moved to whatever is handling health
    }

    public float GetPlayerHealth()
    {
        return currentHealth;
    }
}    
