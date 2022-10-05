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
    public float deathDuration = 4;

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
        UIDamageIn.instance.DamageVis();
        damage -= defence;
        damage = Mathf.Clamp(damage, 0f, Mathf.Infinity);
        switch (true) 
        {
            case bool x when currentHealth - damage > 0:
                currentHealth -= damage;
                break;
            default:
                StartCoroutine(Death());
                break;
        }
    }

    public void healDamage(float healing)
    {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }
    
    public IEnumerator Death()
    {
        invulnerable = true;
        controller._Inactionable = true;
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
