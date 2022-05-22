using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealthTemp : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    //used by IncreasePlayerAttack
    private bool increaseDamage=false;

    [SerializeField] GameObject deathFX;

    private BossManager bossManager;

    private Animator animator;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        bossManager = GetComponent<BossManager>();
    }

    public void takeDamage(int damage){
        if(increaseDamage)
        {
            damage+=2; //we can change this increase to anything
        }
        if (currentHealth > 0){
            currentHealth = currentHealth - damage;
            if (currentHealth >= maxHealth){
                currentHealth = maxHealth;
            }
            UIManager.Instance.HealthBossBarSet(currentHealth);
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }


    public void DamageIncrease()
    {
        if(increaseDamage==true)
        {
            increaseDamage=false;
        }
        else
        {
           increaseDamage=true; 
        }
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2.2f);
        bossManager.enabled = false;
        //Instantiate(deathFX, gameObject.transform.position, Quaternion.identity);
    }
}    
