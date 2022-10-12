using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/Bloodthirst")]

public class Bloodthirst : EventItem
{
    public float lifestealPercent; //base lifesteal value.
    //public float lifestealPerStack; //for multiple item stacking balance. Useful for diminishing returns
    public float damagePercent; //temporary damage increase
    public float speedPercent; //temporary speed increase
    public float duration; //How long it is active
    public float cooldown; //How long before it can be active again.
    public float procPercent; // health to proc when below.
    private bool available; //Is cooldown ready.

    public override void acquire()
    {
        base.acquire();
        available = true;
    }

    public override void subscribe()
    {
        EventManager.instance.OnHealthChange.AddListener(effect);
    }

    public override void effect()
    {
        throw new System.NotImplementedException();
    }

    public void effect(float f) 
    {
        if (available && f <= procPercent) Inventory.instance.StartCoroutine(proc());
    }

    public void effect2(float f)
    {
        //Inventory.instance.StartCoroutine(proc2());
    }

    public IEnumerator proc()
    {
        available = false;
        PlayerHealth hp = GameManager.instance.player.GetComponent<PlayerHealth>();
        //only works for melee.
        BasicAttack dmg = GameManager.instance.player.GetComponent<BasicAttack>();
        //for some reason i can't reference this even though MsBuff does.
        //ThirdPersonController speed = GameManager.instance.player.GetComponent<ThirdPersonController>();

        int currentStacks = Inventory.instance.GetStacks(this);

        float damagePercentIncrease = dmg.damage/100 * (damagePercent * currentStacks); // currently no diminishing returns.
        //float speedPercentIncrease = speed.MoveSpeed/100 * (speedPercent * currentStacks);
        //float sprintSpeedPercentIncrease = speed.SprintSpeed/100 * (speedPercent * currentStacks);
        float lifestealValue = (lifestealPercent / dmg.damage) * 100;

        // speed.MoveSpeed += speedPercentIncrease;
        // speed.SprintSpeed += sprintSpeedPercentIncrease;
        dmg.damage += damagePercentIncrease;

        //EventManager.instance.OnPlayerDamages.AddListener(effect2);

        yield return new WaitForSeconds(duration);
        //reset values once duration ends. 
        // speed.MoveSpeed -= speedPercentIncrease;
        // speed.SprintSpeed -= sprintSpeedPercentIncrease;
        dmg.damage -= damagePercentIncrease;

        yield return new WaitForSeconds(cooldown);
        available = true;
        if ((hp.currentHealth / hp.maxHealth) * 100 <= procPercent) Inventory.instance.StartCoroutine(proc());
    }

    //Lifesteal event occurs
    // public IEnumerator proc2()
    // {
    //     hp.healDamage(lifestealValue);
    //     yield return null;
    // }
}
