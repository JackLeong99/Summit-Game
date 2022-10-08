using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/EmergencyHealth")]

public class EmergencyHealth : EventItem
{
    public float baseHeal;
    public float healPerStack;
    public int numberOfTicks;
    public float tickRate;
    public float cooldown;
    public float procPercent;
    private bool available;

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

    public IEnumerator proc() 
    {
        available = false;
        int currentStacks = Inventory.instance.GetStacks(this);
        float heal = baseHeal + (healPerStack * currentStacks);
        PlayerHealth hp = GameManager.instance.player.GetComponent<PlayerHealth>();
        for (int i = 0; i < numberOfTicks; i++) 
        {
            hp.healDamage(heal);
            yield return new WaitForSeconds(tickRate);
        }
        yield return new WaitForSeconds(cooldown);
        available = true;
        if ((hp.currentHealth / hp.maxHealth) * 100 <= procPercent) Inventory.instance.StartCoroutine(proc());
    }
}
